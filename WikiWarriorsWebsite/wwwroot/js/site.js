
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
//initialliaze the search

//this will add a typing event listener as soon as the page is loaded.
//if no searching is requried on the page (like the homepage) the init function will always just return nothing.
//function initSearch() {
//    const wikiSearchInput = $('searchInput');
//    //look for a valid input input, if there aren't any input sections just return. This allows the script to be included on pages without a search box without breaking.
//    if (!wikiSearchInput)
//    {
//        return;
//    }

//    //debounce function. Stops a user from adding too many input events at once
//    wikiSearchInput.addEventListener('input', () => {
//        debounce(initalCheck(wikiSearchInput.value)), 300);
//    };
//}

async function initialCheck(query) {
    const resultList = document.$('resultList');

    if (!query || query.length < 2) {
        resultList.innerHTML = '<div class="col-12 text-center text-muted"><p>Search results will appear here...</p></div>';
        return;
    }

};

async function fighterExists(Name) {
    const url = "https://localhost:7114/Selection"
    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }

        const result = await response.json();
        console.log(result);
    } catch (error) {
        console.error(error.message);
    }
}
//const url = 'https://en.wikipedia.org/w/api.php?action=query&format=json&titles=' + query



//run the search

//save the results
