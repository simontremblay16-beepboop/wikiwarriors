function $(id){
    return document.getElementById(id);
}

document.addEventListener("DOMContentLoaded", ()=> {

    // https://www.w3schools.com/jsref/dom_obj_event.asp
    $("search").addEventListener("input", wikiSearch) // Search
    $("").addEventListener("click", selectFighter) // Add fighter
    $("").addEventListener("click", deselectFighter) //Deselect
    $("").addEventListener("click", saveFighter) //Save fighter 
    $("").addEventListener("click", startFight) // start fight 
    $("").addEventListener("click", returnHome) // return to homepage
});
    // https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch
    //https://developer.mozilla.org/en-US/docs/Glossary/Debounce

    // type in textbox to create an api request 
    //respond to input event
    //prevent the requests to other input events for a period of time 
let noRequests = false; 

function wikiSearch() {
    // https://www.freecodecamp.org/news/throttling-in-javascript/
    if (noRequests === true) {
        return; 
    }
    noRequests = true;

    let search = $("search").value;

    // https://www.mediawiki.org/wiki/Special:ApiSandbox#action=languagesearch&format=json&search=Maratih&typos=1
    //https://www.mediawiki.org/wiki/Special:ApiSandbox#action=query&format=json&origin=*&prop=pageimages%7Credirects%7Cextracts&generator=search&formatversion=2&piprop=thumbnail%7Cname&pithumbsize=50&exchars=150&exlimit=1&exintro=1&explaintext=1&exsectionformat=wiki&gsrsearch=Jupiter
    //  https://www.mediawiki.org/wiki/API:Query

/*
    : https://en.wikipedia.org/w/api.php?
    action: query //Fetch data from and about MediaWiki.
    generator: search //Perform a full text search.
    gsrsearch: this is were the search is going for from the input. // match this
    format: json
    prop: pageimage, extracts //Returns data
    origin: * //For non-authenticated requests, specify the value *
*/

    let request = `https://www.mediawiki.org/w/api.php?action=query&format=json&origin=*&prop=pageimages%7Credirects%7Cextracts&generator=search&formatversion=2&piprop=thumbnail%7Cname&pithumbsize=50&exchars=150&exlimit=1&exintro=1&explaintext=1&exsectionformat=wiki&gsrsearch=${search}`

    // https://www.w3schools.com/js/js_api_fetch.asp
    let results = fetch(request);





    //// https://www.w3schools.com/jsref/met_win_settimeout.asp
    setTimeout(() => {
        noRequests = false; 
    }, 2000);

    displaySearch(results);
}

function displaySearch (results){
    console.log(results);
}


function selectFighter (){

}

function saveFighter (){

}

function deselectFighter (){

}

function startFight (){

}

function returnHome (){

}


