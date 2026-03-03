// > Helper Function.
function $(id){
    return document.getElementById(id);
}

// > Wait for the DOM to load then create event listeners for each button and the search input. 
document.addEventListener("DOMContentLoaded", ()=> {

    // https://www.w3schools.com/jsref/dom_obj_event.asp
    $("search").addEventListener("input", wikiSearch) // Search
    $("searchButton").addEventListener("click", wikiSearchButton)
    $("displaySearch").addEventListener("click", selectFighter) // Add fighter
    $("saveFighter1").addEventListener("click", saveFighter) //Save fighter 
    $("beginFight").addEventListener("click", startFight) // start fight 
});





    // https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch
    //https://developer.mozilla.org/en-US/docs/Glossary/Debounce

    // type in textbox to create an api request 
    //respond to input event
    //prevent the requests to other input events for a period of time 

    //> Make the API request to get the thumbnail, title, and extracts; Throttle the searches (should probably do debouncement instead); call displaySearch(). 
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
    }, 1000);

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

    //> Get the array out of the results; make the html card to inject to display the image/text.  
    let resultsArray = wikiSearchResults.query.pages;
    console.log(resultsArray);

    //https://www.w3schools.com/jsref/jsref_filter.asp
    let resultsDisplay = resultsArray.filter(search => search.thumbnail?.source)    
    // https://www.w3schools.com/jsref/jsref_forEach.asp
    // https://www.w3schools.com/jsref/jsref_map.asp

    // optional chaining ? 
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

    $("displaySearch").innerHTML = resultsDisplay;
}







//> get the source element of the closest button to the click and display either first 1 of fighter 2 depending on if they've been saved yet. 
let fighterSaved = false;
let wikiFighter1;
let wikiFighter2;
let pageTitle; //! I need to change this later. // It was getting messed up when it was in the other function
function selectFighter (event){
// https://www.w3schools.com/jsref/event_target.asp
// https://developer.mozilla.org/en-US/docs/Web/API/Element/closest

    let closestBtn = event.target.closest(".infoContainer");
    let wikiThumbnail = closestBtn.querySelector(".image");
    pageTitle = closestBtn.querySelector(".textTitle");

    if (fighterSaved == false){

        wikiFighter1 = $("imgID");
        wikiFighter1.src = wikiThumbnail.src;
    }
    else if(fighterSaved == true){

        wikiFighter2 = $("imgID2");
        wikiFighter2.src = wikiThumbnail.src;
    }
    
}






// https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch#:~:text=With%20the%20%EE%80%80Fetch%EE%80%81%20API,%20you%20make%20a%20request

//https://learn.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-10.0

//https://learn.microsoft.com/en-us/aspnet/web-pages/overview/ui-layouts-and-themes/4-working-with-forms

//> get form input; create the form data; fetch() post to the razor page. 
async function saveFighter (){
    const form = $("fighterForm");

    //https://www.w3schools.com/jsref/prop_node_textcontent.asp
    $("fighterTitleInput").value = pageTitle.textContent;

    // https://developer.mozilla.org/en-US/docs/Web/API/FormData/FormData
    const formData = new FormData(form);

    // "" == this page
    const response = await fetch("", {
        method: "POST",
        body: formData
    });

    const result = await response.json();
    //> 

    fighterSaved = true;
}




function startFight (){
    // to save fighter 2 
    saveFighter();
    // Need to redirect to page. 
}
