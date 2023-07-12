
var req;
var reqObj = {};
function GetRequestData(path, parameters) {
    req = parameters;
    reqObj = JSON.parse(parameters);

    var miniRequestRes = miniRequest(path, parameters);
    return miniRequestRes;
}