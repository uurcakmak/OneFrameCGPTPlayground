$(document).ready(function () {
    getLayoutProfilePhoto();

    var menuItem = $("div.menu .menu-item a[href='" + window.location.pathname + "']");
    if (menuItem.length === 0) {
        return;
    }
    menuItem.addClass("active");
    menuParentHighlight(menuItem);
});

function menuParentHighlight(menuItem) {
    var parentMenuItemParent = menuItem.parent("div").parent("div");

    if (parentMenuItemParent.parent("div")[0].id === "kt_header_user_menu_toggle") {
        return;
    }

    if (parentMenuItemParent[0].id === "kt_aside_menu_wrapper" || parentMenuItemParent[0].id === "#kt_aside_menu") {
        return;
    }

    var parentMenuAccordion = parentMenuItemParent.parent("div");
    var parentMenuSub = parentMenuItemParent;
    parentMenuAccordion.addClass("hover show");
    parentMenuSub.addClass("show");
    parentMenuSub.attr("style", "");
    menuParentHighlight(parentMenuAccordion);
}

function getLayoutProfilePhoto() {
    var profilePhotoStorage = localStorage.getItem('PROFILE_PHOTO');
    if ((profilePhotoStorage === null || profilePhotoStorage === "" || profilePhotoStorage === 'undefined')) {
        Common.Ajax("GET", "/profiles/profile-photo", null, "json", function (data) {
            if (data !== undefined) {
                localStorage.setItem('PROFILE_PHOTO', data);
                setLayoutProfilePhoto(data);
            }
        });
    }
    else {
        setLayoutProfilePhoto(profilePhotoStorage);
    }
}

function setLayoutProfilePhoto(profilePhoto) {
    var images = $('[data-id="profilePhoto"]');
    for (var i = 0; i < images.length; i++) {
        images[i].src = profilePhoto;
    }
}
