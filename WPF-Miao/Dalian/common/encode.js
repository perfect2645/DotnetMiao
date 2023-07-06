

function Encode(path, parameters) {
    var parameterObj = JSON.stringify(parameters);
    var miniRequestRes = miniRequest(path, parameters, callback);
    return miniRequestRes;
}
