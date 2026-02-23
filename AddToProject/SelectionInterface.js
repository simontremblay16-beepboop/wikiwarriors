function $(id){
    return document.getElementById(id);
}

document.addEventListener("DOMContentLoaded", ()=> {

    // https://www.w3schools.com/jsref/dom_obj_event.asp
    $("search").addEventListener("input", wikiSearch) // Search
    $("searchButton").addEventListener("click", wikiSearchButton)
    $("displaySearch").addEventListener("click", selectFighter) // Add fighter
    $("displaySearch").addEventListener("click", deselectFighter) //Deselect
    $("displaySearch").addEventListener("click", saveFighter) //Save fighter 
    $("displaySearch").addEventListener("click", startFight) // start fight 
    $("displaySearch").addEventListener("click", returnHome) // return to homepage
});
    // https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch
    //https://developer.mozilla.org/en-US/docs/Glossary/Debounce

    // type in textbox to create an api request 
    //respond to input event
    //prevent the requests to other input events for a period of time 
let noRequests = false; 

async function wikiSearch() {
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

    let request = `https://en.wikipedia.org/w/api.php?action=query&format=json&origin=*&prop=pageimages%7Credirects%7Cextracts&generator=search&formatversion=2&piprop=thumbnail%7Cname&pithumbsize=100&exchars=150&exlimit=max&exintro&explaintext&exchars=450&exsectionformat=wiki&gsrnamespace=0&gsrsearch=${search}`;

    // https://www.w3schools.com/js/js_api_fetch.asp
    // https://www.w3schools.com/js/js_async_await.asp
    let results = await fetch(request);
    let wikiSearchResults = await results.json();

    // https://www.w3schools.com/jsref/met_win_settimeout.asp
    setTimeout(() => {
        noRequests = false; 
    }, 2000);

    displaySearch(wikiSearchResults);
}

function wikiSearchButton (){
    noRequests = false;
    wikiSearch();
}


// remember copy path in console. 
function displaySearch (wikiSearchResults){

    // if it returns a page then do this if not wait
    // do not display if no image source is present 
    // add a button than can allow do the function 
    // I need to turn each div into a button so be chosen // make it so something when hover over so people know it's clickable 

    let resultsArray = wikiSearchResults.query.pages;
    console.log(resultsArray);

    let displayCard = 

    //https://www.w3schools.com/jsref/jsref_filter.asp
    resultsArray.filter(search => search.thumbnail?.source)
    // https://www.w3schools.com/jsref/jsref_forEach.asp
    // https://www.w3schools.com/jsref/jsref_map.asp

    // optional chaining ? 
    
    //template literals
    .map(
        search => {
            return(
                `<button class="infoContainer">
                    <img class="image" src="${search.thumbnail?.source}">  
            
                    <div class="infoSection">
                        <h4 class="textTitle">${search.title}</h4>   
                        <p class="text">${search.extract}</p>
                    </div>

                </button>`
            )
        }
        // https://www.w3schools.com/jsref/jsref_join.asp
    ).join('');

    $("displaySearch").innerHTML = displayCard;
}


function selectFighter (){
    //https://www.w3schools.com/jsref/met_node_clonenode.asp#:~:text=The%20cloneNode%28%29%20method%20creates%20a%20copy%20of%20a,Default.%20Clone%20only%20the%20node%20and%20its%20attributes.
}

function saveFighter (){

}

function deselectFighter (){

}

function startFight (){

}

function returnHome (){

}


