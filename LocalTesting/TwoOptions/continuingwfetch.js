// https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch#:~:text=The%20Fetch%20API%20provides%20a%20JavaScript%20interface
// https://www.learnrazorpages.com/razor-pages/handler-methods
// This will be put in it's own .js file 
// when the begin fight button is clicked it will call the handler to insert into the database 
// no reload. 

async function CallInsertFighterHistory() {
    const response = await fetch("", { //pageName ?handler = handlerName
        method: 'POST',
        headers: {'Content-Type': 'application/json'}//add verification token 
    });
    const result = await response.json();
}





/*
Server Side code: 



*/

