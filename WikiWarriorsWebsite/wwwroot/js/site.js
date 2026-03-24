
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

   // console.log(gifss(1));
   // console.log(gifss(2));

    //if no fighters have been selected make the buttons add the first figther ID
    if (!sessionStorage.firstID) {
        let buttonArr = $$("cardRadio");
        buttonArr.forEach(thing => {
            thing.addEventListener("click", (evt) => {
                sessionStorage.setItem("firstID", evt.target.value);
                $("FOneID").value = evt.target.value;
                console.log(evt.target.value);
                console.log(gifss(1));

            });

        });
    }
    //if the first fighter has been selected, make the buttons add the second figther ID instead
    else if (!sessionStorage.secondID) {
        let buttonArr = $$("cardRadio");
        buttonArr.forEach(thing => {
            thing.addEventListener("click", (evt) => {
                sessionStorage.setItem("secondID", evt.target.value);
                $("FTwoID").value = evt.target.value;
                console.log(evt.target.value);
                console.log(gifss(2));
            });

        });
    }
    else {

        console.log(gifss(1))
        console.log(gifss(2))
    }
});

function saveSessioninfo() {
    $("FOneID").value = gifss(1);
    $("FTwoID").value = gifss(2);
}   

function getTFA() {
    let today = new Date();
    let year = today.getFullYear();
    let month = String(today.getMonth() + 1).padStart(2, '0');
    let day = String(today.getDate()).padStart(2, '0');
    let url = `https://api.wikimedia.org/feed/v1/wikipedia/en/featured/${year}/${month}/${day}`;

    let response = await fetch(url,
        {
            headers: {
                'Authorization': 'Bearer YOUR_ACCESS_TOKEN',
                'Api-User-Agent': 'WikiWarriorsWebsite/1.0 (strembl6@confederationcollege.ca)'
            }
        }
    );
}



