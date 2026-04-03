//console.log("Hello is this on?")
sessionStorage.clear();

//// Content loaded listenter
//document.addEventListener("DOMContentLoaded", documentReady);

//function FormatDateAsStr(fullDateTime) {
//    //const fullDateTime = date;
//    // Format the current date to match the format from the database (i.e. leading zeroes, Year Month Day)
//    year = fullDateTime.getFullYear().toString();
//    if (year.length < 2) {
//        year = "0" + year;
//    }
//    // months are number from 0 by default, so we add 1
//    month = (fullDateTime.getMonth() + 1).toString();
//    if (month.length < 2) {
//        month = "0" + month;
//    }
//    dayOfMonth = fullDateTime.getDate().toString();
//    if (dayOfMonth.length < 2) {
//        dayOfMonth = "0" + dayOfMonth;
//    }
//    const formattedDate = year + "-" + month + "-" + dayOfMonth;
//    return formattedDate;
//}

//// document ready event handler
//// called automatically when pages done loading
//function documentReady() {
//    // If the createDaily parameter is shown in the URL, remove it, because we will have already
//    // added a new daily fight and don't want reloading the page to create another.
//    let URLparameters = new URLSearchParams(document.location.search);
//    if (URLparameters.get("createDaily")) {
//        window.location.replace("/Index");
//    }
//    // Get the current date and compare to the last Daily fight date to
//    // figure out if we need to create a NEW daily fight

//    const currentDate = FormatDateAsStr(new Date());

//    // This will extract only the date part from the dailyfightDate string, ignoring the time.
//    const lastDate = document.getElementById("dailyFightDate").innerHTML.split(" ")[0];

//    // Debug info display
//    document.getElementById("currentDate").innerHTML = currentDate;
//    document.getElementById("lastDate").innerHTML = lastDate;

//    // Check if current date does not match last daily fight date
//    if (currentDate != lastDate) {
//        // If not, then we will redirect with a URL variable to indicate
//        // that we DO need to create a new daily fight

//        window.location.replace("/Index?createDaily=true"); // NOTE: Comment out this line to disable daily fights
//    }
//}