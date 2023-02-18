var cloakSpan = document.getElementById("cloakCounter");
var stoneSpan = document.getElementById("stoneCounter");
var wandSpan = document.getElementById("wandCounter");
//create connection to our signalR hub with the given route
/*.configureLogging(signalR.LogLevel.debug)*/
var connectionDealthyHallow = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/deathlyhallowshub", signalR.HttpTransportType.WebSockets).build();

//connect to methods that hub invokes aka receive notfications from hub
connectionDealthyHallow.on("UpdateDeathlyHallowsCount", (cloak, stone, wand) => {
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
});

//invoke hub methods aka send notification to hub

//start connection
function fulfilled() {
    connectionDealthyHallow.invoke("GetRaceStatus").then((raceCounter) => {
        cloakSpan.innerText = raceCounter.cloak.toString();
        stoneSpan.innerText = raceCounter.stone.toString();
        wandSpan.innerText = raceCounter.wand.toString();
    });
    //do something on start
    console.log("Connection to User Hub Successful");
}
function rejected() {
    //rejected logs
}

connectionDealthyHallow.start().then(fulfilled, rejected);