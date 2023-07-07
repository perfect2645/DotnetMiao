
var parameterObj = {};
function Encode(path, parameters) {
    parameterObj = JSON.stringify(parameters);
    var miniRequestRes = miniRequest(path, parameters, callback);
    return miniRequestRes;
}
