function $(id) {
    return document.getElementById(id);
}

// Content loaded listenter
document.addEventListener("DOMContentLoaded", documentReady);

function Selection(imageUrl, pageId, title) {

    const activeSlot = sessionStorage.getItem("ActiveFighterSlot");

    if (activeSlot == "1") {
        sessionStorage.setItem("ImageUrl1", imageUrl);
        sessionStorage.setItem("PageId1", pageId);
        sessionStorage.setItem("Title1", title);

        $("img1").src = imageUrl;
        $("fighterTitle1").textContent = title;
        $("fighter1Id").value = pageId;

        sessionStorage.removeItem("ActiveFighterSlot");
        $("fighterCard1").classList.remove("selectedSlot");
        return;
    }

    if (activeSlot == "2") {
        sessionStorage.setItem("ImageUrl2", imageUrl);
        sessionStorage.setItem("PageId2", pageId);
        sessionStorage.setItem("Title2", title);

        $("img2").src = imageUrl;
        $("fighterTitle2").textContent = title;
        $("fighter2Id").value = pageId;

        sessionStorage.removeItem("ActiveFighterSlot");
        document.getElementById("fighterCard2").classList.remove("selectedSlot");
        return;
    }

    if (!sessionStorage.getItem("ImageUrl1")) {
        sessionStorage.setItem("ImageUrl1", imageUrl);
        sessionStorage.setItem("PageId1", pageId);
        sessionStorage.setItem("Title1", title);

        $("img1").src = imageUrl;
        $("fighterTitle1").textContent = title;
        $("fighter1Id").value = pageId;
    }
    else if (!sessionStorage.getItem("ImageUrl2")) {
        sessionStorage.setItem("ImageUrl2", imageUrl);
        sessionStorage.setItem("PageId2", pageId);
        sessionStorage.setItem("Title2", title);

        $("img2").src = imageUrl;
        $("fighterTitle2").textContent = title;
        $("fighter2Id").value = pageId;
    }
}

function SelectFighterSlot(slotNumber) {
    sessionStorage.setItem("ActiveFighterSlot", slotNumber);

    $("fighterCard1").classList.remove("selectedSlot");
    $("fighterCard2").classList.remove("selectedSlot");

    if (slotNumber == 1) {
        $("fighterCard1").classList.add("selectedSlot");
    }
    else if (slotNumber == 2) {
        $("fighterCard2").classList.add("selectedSlot");
    }
}

function documentReady() {

    const imageUrl1 = sessionStorage.getItem("ImageUrl1");
    const imageUrl2 = sessionStorage.getItem("ImageUrl2");
    const title1 = sessionStorage.getItem("Title1");
    const title2 = sessionStorage.getItem("Title2");
    const pageId1 = sessionStorage.getItem("PageId1");
    const pageId2 = sessionStorage.getItem("PageId2");

    if (imageUrl1) {
        $("img1").src = imageUrl1;
    }
    if (imageUrl2) {
        $("img2").src = imageUrl2;
    }

    if (title1) {
        $("fighterTitle1").textContent = title1;
    }
    if (title2) {
        $("fighterTitle2").textContent = title2;
    }

    if (pageId1) {
        $("fighter1Id").value = pageId1;
    }
    if (pageId2) {
        $("fighter2Id").value = pageId2;
    }
}

function ClearSessionStorage() {

    sessionStorage.clear();

    document.getElementById("fighterTitle1").textContent = "Fighter 1";
    document.getElementById("fighterTitle2").textContent = "Fighter 2";

    document.getElementById("fighter1Id").value = "";
    document.getElementById("fighter2Id").value = "";

    document.getElementById("img1").src = "/SelectionPlaceholder.png";
    document.getElementById("img2").src = "/SelectionPlaceholder.png";

    sessionStorage.setItem("ActiveFighterSlot", 1);
    slotNumber = 1

}

// Prevent start error without both fighters
document.querySelector(".makeFightButton").addEventListener("click", function (e) {
    const url1 = sessionStorage.getItem("ImageUrl1");
    const url2 = sessionStorage.getItem("ImageUrl2");

    if (!url1 || !url2) {
        e.preventDefault();
        e.stopImmediatePropagation();
    }
});