﻿using AutoMapper;
using Htmx;
using Market.Domain.Models.Dto.Services.Auth;
using Market.Web.Service.IService;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Market.Web.Pages.Auth
{
    public class IndexModel : PageModel
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true)]
        public SignInRequestDto SignInRequest { get; set; } = new SignInRequestDto();
        [BindProperty(SupportsGet = true)]
        public SignUpRequestDto SignUpRequest { get; set; } = new SignUpRequestDto();
        public List<SelectListItem> Roles { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ToastTitle { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string ToastBody { get; set; }

        public IndexModel(IAuthService authService, IMapper mapper, ITokenProvider tokenProvider)
        {
            _mapper = mapper;
            _authService = authService;
            _tokenProvider = tokenProvider;

            var response = _authService.GetRolesAsync().GetAwaiter().GetResult();
            JArray jsonResponse = JArray.Parse(JsonConvert.SerializeObject(response.Data));
            Roles = new List<SelectListItem>();

            foreach (var item in jsonResponse)
            {
                JObject role = (JObject)item;
                Roles.Add(JsonConvert.DeserializeObject<SelectListItem>(role.ToString()));
            }
        }

        public async Task<IActionResult> OnGetRoles()
        {
            var response = await _authService.GetRolesAsync();
            JArray jsonResponse = JArray.Parse(JsonConvert.SerializeObject(response.Data));
            Roles = new List<SelectListItem>();

            foreach (var item in jsonResponse)
            {
                JObject role = (JObject)item;
                Roles.Add(JsonConvert.DeserializeObject<SelectListItem>(role.ToString()));
            }

            if (!Request.IsHtmx())
                return Page();

            Response.Htmx(h =>
            {
                h.PushUrl(Request.GetEncodedUrl());
            });
            return Partial("./Auth/Modal", this);
        }

        public async Task<IActionResult> OnPostSignIn()
        {
            TempData.Clear();
            var response = await _authService.SignInAsync(SignInRequest);
            if (!response.IsSuccess)
            {
                ToastTitle = "Sign In Failed";
                response.Metadata.ToList().ForEach(x => TempData.Add(x, x));

                return RedirectToPage("Index", this);
            }

            ToastTitle = "Sign In Successful";
            var data = JsonConvert.DeserializeObject<SignInResponseDto>(((JObject)response.Data).ToString());
            _tokenProvider.SetToken(data.Token);
            TempData.Add("name", $"Welcome {data.User.Name}");

            return RedirectToPage("Index", this);
        }

        public async Task<IActionResult> OnPostSignUp()
        {
            TempData.Clear();

            var response = await _authService.SignUpAsync(SignUpRequest);
            if (!response.IsSuccess)
            {
                ToastTitle = response.Message;
                response.Metadata.ToList().ForEach(x => TempData.Add(x, x));
                return RedirectToPage("Index", this);
            }
            ToastTitle = "Sign Up Successful";
            TempData.Add("signup-result", response.Message);

            var roleRes = await _authService.AssignRoleAsync(_mapper.Map<RoleRequestDto>(SignUpRequest));
            if (!roleRes.IsSuccess)
            {
                TempData.Add("assignrole-result", roleRes.Message);
                return RedirectToPage("Index", this);
            }
            TempData.Add("assignrole-result", roleRes.Message);

            return RedirectToPage("Index", this);
        }

    }
}
