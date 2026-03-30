
// Content loaded listenter
document.addEventListener("DOMContentLoaded", documentReady);

function Selection(imageUrl, pageId, title) {

    const activeSlot = sessionStorage.getItem("ActiveFighterSlot");

    if (activeSlot == "1") {
        sessionStorage.setItem("ImageUrl1", imageUrl);
        sessionStorage.setItem("PageId1", pageId);
        sessionStorage.setItem("Title1", title);

        document.getElementById("img1").src = imageUrl;
        document.getElementById("fighterTitle1").textContent = title;
        document.getElementById("fighter1Id").value = pageId;

        sessionStorage.removeItem("ActiveFighterSlot");
        document.getElementById("fighterCard1").classList.remove("selectedSlot");
        return;
    }

    if (activeSlot == "2") {
        sessionStorage.setItem("ImageUrl2", imageUrl);
        sessionStorage.setItem("PageId2", pageId);
        sessionStorage.setItem("Title2", title);

        document.getElementById("img2").src = imageUrl;
        document.getElementById("fighterTitle2").textContent = title;
        document.getElementById("fighter2Id").value = pageId;

        sessionStorage.removeItem("ActiveFighterSlot");
        document.getElementById("fighterCard2").classList.remove("selectedSlot");
        return;
    }

    if (!sessionStorage.getItem("ImageUrl1")) {
        sessionStorage.setItem("ImageUrl1", imageUrl);
        sessionStorage.setItem("PageId1", pageId);
        sessionStorage.setItem("Title1", title);

        document.getElementById("img1").src = imageUrl;
        document.getElementById("fighterTitle1").textContent = title;
        document.getElementById("fighter1Id").value = pageId;
    }
    else if (!sessionStorage.getItem("ImageUrl2")) {
        sessionStorage.setItem("ImageUrl2", imageUrl);
        sessionStorage.setItem("PageId2", pageId);
        sessionStorage.setItem("Title2", title);

        document.getElementById("img2").src = imageUrl;
        document.getElementById("fighterTitle2").textContent = title;
        document.getElementById("fighter2Id").value = pageId;
    }
}

function SelectFighterSlot(slotNumber) {
    sessionStorage.setItem("ActiveFighterSlot", slotNumber);

    document.getElementById("fighterCard1").classList.remove("selectedSlot");
    document.getElementById("fighterCard2").classList.remove("selectedSlot");

    if (slotNumber == 1) {
        document.getElementById("fighterCard1").classList.add("selectedSlot");
    }
    else if (slotNumber == 2) {
        document.getElementById("fighterCard2").classList.add("selectedSlot");
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
        document.getElementById("img1").src = imageUrl1;
    }
    if (imageUrl2) {
        document.getElementById("img2").src = imageUrl2;
    }

    if (title1) {
        document.getElementById("fighterTitle1").textContent = title1;
    }
    if (title2) {
        document.getElementById("fighterTitle2").textContent = title2;
    }

    if (pageId1) {
        document.getElementById("fighter1Id").value = pageId1;
    }
    if (pageId2) {
        document.getElementById("fighter2Id").value = pageId2;
    }
}

function ClearSessionStorage() {
    sessionStorage.removeItem("ImageUrl1");
    sessionStorage.removeItem("PageId1");
    sessionStorage.removeItem("Title1");

    sessionStorage.removeItem("ImageUrl2");
    sessionStorage.removeItem("PageId2");
    sessionStorage.removeItem("Title2");

    document.getElementById("img1").src = "";
    document.getElementById("img2").src = "";

    document.getElementById("fighterTitle1").textContent = "Fighter 1";
    document.getElementById("fighterTitle2").textContent = "Fighter 2";

    document.getElementById("fighter1Id").value = "";
    document.getElementById("fighter2Id").value = "";
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



// old functional code
////Helpful functions!
////returns the element by id
//function $(a) { return document.getElementById(a); }
////returns the elements by class in an array
//function $$(a) { return Array.from(document.getElementsByClassName(a)); }

//// Very helpful article for understanding debounce, rest arguments & spread syntax :D
//// https://levelup.gitconnected.com/debounce-from-scratch-8616c8209b54

//function gifss(a) {
//    if (a == 1) {
//        return sessionStorage.getItem("firstID");
//    }
//    else if (a == 2){
//        return sessionStorage.getItem("secondID");
//    }
//    else {
//        return "lookup failed!";
//    }

//}

//const debounce = (callback, delay) => {
//    let timer;

//    return (...args) => {
//        clearTimeout(timer);
//        timer = setTimeout(() => callback(...args), delay);
//    };
//};

//document.addEventListener("DOMContentLoaded", () => {

//   // console.log(gifss(1));
//   // console.log(gifss(2));

//    //if no fighters have been selected make the buttons add the first figther ID
//    if (!sessionStorage.firstID) {
//        let buttonArr = $$("cardRadio");
//        buttonArr.forEach(thing => {
//            thing.addEventListener("click", (evt) => {
//                sessionStorage.setItem("firstID", evt.target.value);
//                $("FOneID").value = evt.target.value;
//                console.log(evt.target.value);
//                console.log(gifss(1));

//            });

//        });
//    }
//    //if the first fighter has been selected, make the buttons add the second figther ID instead
//    else if (!sessionStorage.secondID) {
//        let buttonArr = $$("cardRadio");
//        buttonArr.forEach(thing => {
//            thing.addEventListener("click", (evt) => {
//                sessionStorage.setItem("secondID", evt.target.value);
//                $("FTwoID").value = evt.target.value;
//                console.log(evt.target.value);
//                console.log(gifss(2));
//            });

//        });
//    }
//    else {

//        console.log(gifss(1))
//        console.log(gifss(2))
//    }
//});

//function saveSessioninfo() {
//    $("FOneID").value = gifss(1);
//    $("FTwoID").value = gifss(2);
//}   
