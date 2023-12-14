document.body.addEventListener('htmx:configRequest', function (evt) {
    // not needed for GET requests
    if (evt.detail.verb === 'GET') {
        return;
    }

    let antiForgeryRequestToken = document.querySelector('input[name="__RequestVerificationToken"]')[0];
    if (antiForgeryRequestToken) {
        evt.detail.parameters['__RequestVerificationToken'] = antiForgeryRequestToken.value;
        console.log(antiForgeryRequestToken);
    }
});

function showModal() {
    const backdrop = document.getElementById("modal-backdrop");
    const modal = document.getElementById("modal");

    setTimeout(() => {
        modal.classList.add("show")
        backdrop.classList.add("show")
    }, 10);
}

function closeModal() {
    const container = document.getElementById("product-modal-container");
    const backdrop = document.getElementById("modal-backdrop");
    const modal = document.getElementById("modal");

    modal.classList.remove("show")
    backdrop.classList.remove("show")

    setTimeout(function () {
        container.removeChild(backdrop)
        container.removeChild(modal)
    }, 200)
}

function closeItemModal(callback) {
    const container = document.getElementById("itemModalContainer");
    const modal = document.getElementById("itemModal");
    const backdrop = document.getElementById("itemModalBackdrop");
    if (modal)
        modal.classList.remove("show");
    if (backdrop)
        backdrop.classList.remove("show");
    setTimeout(function () {
        if (backdrop)
            container.removeChild(backdrop);
        if (modal)
            container.removeChild(modal);
        if (callback)
            callback();
    }, 200);
}

function closeConfirmDeleteModal(callback) {
    document.getElementById("confirmDeleteModal").modal("hide");
    if (callback) {
        setTimeout(function () {
            callback();
        }, 200);
    }
}

function errorRefresh(details) {
    alert("Sorry, an error occurred. The page will now refresh. Please try again.");
    location.reload();
}

document.body.addEventListener("htmx:responseError", function (e) {
    errorRefresh(e.details);
});

document.body.addEventListener("htmx:onLoadError", function (e) {
    errorRefresh(e.details);
});

document.body.addEventListener('htmx:configRequest', function (e) {
    if (e.detail.verb === "delete") {
        if (e.detail.parameters.deleteItemId) {
            let deleteUrl = new URL(e.detail.path, location.origin);
            if (!deleteUrl.searchParams.get('id')) {
                deleteUrl.searchParams.set('id', e.detail.parameters.deleteItemId);
            }
            e.detail.path = deleteUrl.pathname + deleteUrl.search;
        }
    }
});

document.body.addEventListener("gridItemEdit", function () {
    closeItemModal(() => htmx.ajax("GET", document.location.href));
});

document.body.addEventListener("gridItemDelete", function () {
    closeConfirmDeleteModal(() => htmx.ajax("GET", document.location.href));
});

function isFormValid(submitter) {
    const form = document.getElementsById(submitter).closest("form");
    if (document.getElementsById(form).data("submitted")) {
        return false;
    }
    else {
        const isValid = form.valid();
        if (isValid)
            document.getElementsById(form).data("submitted", true);
        return isValid;
    }
}

function preventMultiSubmit(form) {
    if (document.getElementsById(form).data("submitted")) {
        return false;
    }
    else {
        document.getElementsById(form).data("submitted", true);
        return true;
    }
}

function confirmDelete(id, itemName) {
    document.getElementsById("deleteItemName").text(itemName);
    document.getElementsById("deleteItemId").attr("value", id);
    document.getElementsById("confirmDeleteModal").modal();
}