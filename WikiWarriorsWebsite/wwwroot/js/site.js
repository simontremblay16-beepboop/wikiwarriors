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

        clearFightWarning();
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

        clearFightWarning();
        sessionStorage.removeItem("ActiveFighterSlot");
        $("fighterCard2").classList.remove("selectedSlot");
        return;
    }

    if (!sessionStorage.getItem("ImageUrl1")) {
        sessionStorage.setItem("ImageUrl1", imageUrl);
        sessionStorage.setItem("PageId1", pageId);
        sessionStorage.setItem("Title1", title);

        $("img1").src = imageUrl;
        $("fighterTitle1").textContent = title;
        $("fighter1Id").value = pageId;
        clearFightWarning();
    }
    else if (!sessionStorage.getItem("ImageUrl2")) {
        sessionStorage.setItem("ImageUrl2", imageUrl);
        sessionStorage.setItem("PageId2", pageId);
        sessionStorage.setItem("Title2", title);

        $("img2").src = imageUrl;
        $("fighterTitle2").textContent = title;
        $("fighter2Id").value = pageId;
        clearFightWarning();
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

function clearFightWarning() {
    $("fightWarning").textContent = "";

    $("fighterCard1").classList.remove("cardWarning");
    $("fighterCard2").classList.remove("cardWarning");
}

function showFightWarning(message, warnCard1, warnCard2) {
    $("fightWarning").textContent = message;

    $("fighterCard1").classList.toggle("cardWarning", warnCard1);
    $("fighterCard2").classList.toggle("cardWarning", warnCard2);
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

    // Prevent start error without both fighters
    const makeFightButton = document.querySelector(".makeFightButton");

    if (makeFightButton) {
        makeFightButton.addEventListener("click", function (e) {
            const url1 = sessionStorage.getItem("ImageUrl1");
            const url2 = sessionStorage.getItem("ImageUrl2");

            const warning = $("fightWarning");
            const fighterCard1 = $("fighterCard1");
            const fighterCard2 = $("fighterCard2");

            if (warning) {
                warning.textContent = "";
            }
            if (fighterCard1) {
                fighterCard1.classList.remove("cardWarning");
            }
            if (fighterCard2) {
                fighterCard2.classList.remove("cardWarning");
            }

            if (!url1 || !url2) {
                e.preventDefault();

                if (warning) {
                    if (!url1 && !url2) {
                        warning.textContent = "Please select two fighters before starting the fight!";
                    }
                    else if (!url1) {
                        warning.textContent = "Please select fighter 1 before starting a fight!";
                    }
                    else if (!url2) {
                        warning.textContent = "Please select fighter 2 before starting a fight!";
                    }
                }
                if (!url1 && fighterCard1) {
                    fighterCard1.classList.add("cardWarning");
                }
                if (!url2 && fighterCard2) {
                    fighterCard2.classList.add("cardWarning");
                }
            }
        });
    }
}

function ClearSessionStorage() {
    sessionStorage.clear();


    $("fighterTitle1").textContent = "Fighter 1";
    $("fighterTitle2").textContent = "Fighter 2";

    $("fighter1Id").value = "";
    $("fighter2Id").value = "";

    $("img1").src = "/SelectionPlaceholder.png";
    $("img2").src = "/SelectionPlaceholder.png";

    clearFightWarning();

    $("fighterCard1").classList.remove("selectedSlot");
    $("fighterCard2").classList.remove("selectedSlot");

    sessionStorage.setItem("ActiveFighterSlot", 1);
}