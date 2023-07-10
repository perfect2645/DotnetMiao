
var parameterObj = {};
function Encode(path, parameters) {
    var miniRequestRes = miniRequest(path, parameters);
    return miniRequestRes;
}
