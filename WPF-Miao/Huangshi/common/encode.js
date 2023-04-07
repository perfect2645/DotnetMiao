var getEncodeString = function (srcString) {
    var BASE32CHAR = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
    var i = 0;
    var index = 0;
    var digit = 0;
    var currByte;
    var nextByte;
    var retrunString = "";

    for (var i = 0; i < srcString.length;) {
        //var          index    = 0;
        currByte =
            srcString.charCodeAt(i) >= 0
                ? srcString.charCodeAt(i)
                : srcString.charCodeAt(i) + 256;

        if (index > 3) {
            if (i + 1 < srcString.length) {
                nextByte =
                    srcString.charCodeAt(i + 1) >= 0
                        ? srcString.charCodeAt(i + 1)
                        : srcString.charCodeAt(i + 1) + 256;
            } else {
                nextByte = 0;
            }

            digit = currByte & (0xff >> index);
            index = (index + 5) % 8;
            digit <<= index;
            digit |= nextByte >> (8 - index);
            i++;
        } else {
            digit = (currByte >> (8 - (index + 5))) & 0x1f;
            index = (index + 5) % 8;

            if (index == 0) {
                i++;
            }
        }

        retrunString = retrunString + BASE32CHAR.charAt(digit);
    }
    return retrunString.toLowerCase();
};
