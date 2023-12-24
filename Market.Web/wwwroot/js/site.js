﻿if (!document.body.attributes.__htmx_antiforgery) {
    document.addEventListener("htmx:configRequest", evt => {
        let httpVerb = evt.detail.verb.toUpperCase();
        if (httpVerb === 'GET') return;
        let antiForgery = htmx.config.antiForgery;
        if (antiForgery) {
            // already specified on form, short circuit
            if (evt.detail.parameters[antiForgery.formFieldName])
                return;

            if (antiForgery.headerName) {
                evt.detail.headers[antiForgery.headerName]
                    = antiForgery.requestToken;
            } else {
                evt.detail.parameters[antiForgery.formFieldName]
                    = antiForgery.requestToken;
            }
        }
    });
    document.addEventListener("htmx:afterOnLoad", evt => {
        if (evt.detail.boosted) {
            const parser = new DOMParser();
            const html = parser.parseFromString(evt.detail.xhr.responseText, 'text/html');
            const selector = 'meta[name=htmx-config]';
            const config = html.querySelector(selector);
            if (config) {
                const current = document.querySelector(selector);
                // only change the anti-forgery token
                const key = 'antiForgery';
                htmx.config[key] = JSON.parse(config.attributes['content'].value)[key];
                // update DOM, probably not necessary, but for sanity's sake
                current.replaceWith(config);
            }
        }
    });
    document.body.attributes.__htmx_antiforgery = true;
}


function showModal() {
    const modal = new bootstrap.Modal('#my-modal');
    modal.show();
}

function closeModal() {
    const container = document.getElementById("modal-container");
    const backdrop = document.getElementById("modal-backdrop");
    const modal = document.getElementById("my-modal");

    modal.classList.remove("show")
    backdrop.classList.remove("show")

    setTimeout(function () {
        container.removeChild(backdrop)
        container.removeChild(modal)
    }, 200);
}

function showModalAuth() {
    const modal = new bootstrap.Modal('#auth-my-modal');
    modal.show();
}

function closeModalAuth() {
    const container = document.getElementById("auth-modal-container");
    const backdrop = document.getElementById("auth-modal-backdrop");
    const modal = document.getElementById("auth-my-modal");

    backdrop.classList.remove("show")
    modal.classList.remove("show")    

    backdrop.classList.remove("fade")
    modal.classList.remove("fade")   

    setTimeout(function () {
        container.removeChild(backdrop)
        container.removeChild(modal)
    }, 200)
}