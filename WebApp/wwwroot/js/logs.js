"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/realTimeLogs").build();

connection.on("ReceiveMessage", function (logMessage) {
    var localSeqNum = seqNum++;
    var ok = true;
    var msg = '';
    if (!isStringNullOrEmpty(logMessage)) {
        msg += logMessage + '<br/>';
    }
    else {
        msg += 'The log message coming from backend is empty.' + '<br/>';
    }
    logMsg(msg, localSeqNum, ok);
});

connection.start().then(function () {
    //connection.invoke("SendLogAsync", "The connection to the backend has started.").catch(function (err) {
    //    return console.error(err.toString());
    //});
    console.log('The connection to the backend has started.');
}).catch(function (err) {
    return console.error(err.toString());
});