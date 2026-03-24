
//Helpful functions!
//returns the element by id
function $(a) { return document.getElementById(a); }
//returns the elements by class in an array
function $$(a) { return Array.from(document.getElementsByClassName(a)); }

// Very helpful article for understanding debounce, rest arguments & spread syntax :D
// https://levelup.gitconnected.com/debounce-from-scratch-8616c8209b54

function gifss(a) {
    if (a == 1) {
        return sessionStorage.getItem("firstID");
    }
    else if (a == 2){
        return sessionStorage.getItem("secondID");
    }
    else {
        return "lookup failed!";
    }

}

const debounce = (callback, delay) => {
    let timer;

    return (...args) => {
        clearTimeout(timer);
        timer = setTimeout(() => callback(...args), delay);
    };
};

document.addEventListener("DOMContentLoaded", () => {
    //if no fighters have been selected make the buttons add the first figther ID
    if (!sessionStorage.firstID) {
        let buttonArr = $$("cardRadio");
        buttonArr.forEach(thing => {
            thing.addEventListener("click", (evt) => {
                sessionStorage.setItem("firstID", evt.target.value);
                console.log(evt.target.value);
            });

        });
    }
    //if the first fighter has been selected, make the buttons add the second figther ID instead
    else if (!sessionStorage.secondID) {
        let buttonArr = $$("cardRadio");
        buttonArr.forEach(thing => {
            thing.addEventListener("click", (evt) => {
                sessionStorage.setItem("secondID", evt.target.value);
                console.log(evt.target.value);
            });

        });
    }
    else {

        console.log(gifss(1))
        console.log(gifss(2))
    }
});

//<form method="post" onsubmit="saveSessioninfo()">
//    <input type="hidden" id="hiddenSessionData" name="SessionData" />
//    <button type="submit">Submit</button>
//</form>
function saveSessioninfo() {
    const dataOne = gifss(1);
    const dataTwo = gifss(2);
    $("FOneID").value = dataOne;
    $("FTwoID").value = dataTwo;
}   




