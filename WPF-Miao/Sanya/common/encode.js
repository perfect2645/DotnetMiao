﻿global.Buffer = global.Buffer || require('buffer').Buffer;

if (typeof btoa === 'undefined') {
    global.btoa = function (str) {
        return new Buffer.from(str).toString('base64');
    };
}
if (typeof atob === 'undefined') {
    global.atob = function (b64Encoded) {
        return new Buffer.from(b64Encoded, 'base64').toString();
    };
}

function GetZoeParams(encodeParam) {
    var decodeParams = JSON.parse(
        decodeURIComponent(
            escape(
                atob(
                    encodeParam
                )
            )
        )
    );

    return decodeParams;
}