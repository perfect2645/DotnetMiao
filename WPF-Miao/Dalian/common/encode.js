
var req;
var parameterObj = {};
var UserId;
function GetRequestData(path, userId, parameters) {
    req = parameters;
    UserId = userId;
    parameterObj = JSON.parse(parameters);
    var miniRequestRes = miniRequest(path, parameterObj);
    return miniRequestRes;
}