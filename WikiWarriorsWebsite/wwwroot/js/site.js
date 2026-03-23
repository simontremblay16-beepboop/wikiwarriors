
//Helpful functions!
//returns the element by id
function $(a) { return document.getElementById(a); }
//returns the element by class 
function $$(a) { return document.getElementsByClassName(a); }

// Very helpful article for understanding debounce, rest arguments & spread syntax :D
// https://levelup.gitconnected.com/debounce-from-scratch-8616c8209b54

const debounce = (callback, delay) => {
    let timer;

    return (...args) => {
        clearTimeout(timer);

        timer = setTimeout(() => callback(...args), delay);
    };
};

document.addEventListener("DOMContentLoaded", () => {

    if (!sessionStorage.firstID) {
        $$("btnSelection").addEventListener("click", (e) => {

            sessionStorage.setItem("firstID", e.);
            

        });
    }
    else if (!sessionStorage.secondID)
    {
        $$("btnSelection").addEventListener("click", setIDTwo);
    }

    let selectedID = getCookieValue("id");

    if (id) {
        let cookieString;
        cookieString = `Your name: ${name} || Your Favourite Cookie: ${favC}`;
        $("pOutput").innerHTML = cookieString;
    }
});

function getCookieValue(key) {
    //pain 
    let temp = document.cookie.split("; ");
    temp = temp.find((pair) => pair.startsWith(`${key}`))?.split("=")[1];
    return temp
}
