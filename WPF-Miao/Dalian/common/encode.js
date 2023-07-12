
var parameterObj = {};
var reqObj = {};
function Encode(path, parameters) {

    reqObj = JSON.parse(parameters);

    var miniRequestRes = miniRequest(path, parameters);
    return miniRequestRes;
}