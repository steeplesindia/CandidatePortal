export function download(options) {

    // Create the URL
    //const file = new File([options.content], option.filename, { type: option.contentType });
    //const exportUrl = URL.createObjectURL(file);

    //// Create the <a> element and click on it
    //const a = document.createElement("a");
    //document.body.appendChild(a);
    //a.href = exportUrl;
    //a.download = option.filename;
    //a.target = "_self";
    //a.click();

    //URL.revokeObjectURL(exportUrl);

    var url = "data:" + options.contentType + ";base64," + options.byteArray
    var anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = options.fileName;
    anchorElement.click();
    anchorElement.remove();
}


function downloadFromByteArray(options: {
    byteArray: string,
    fileName: string,
    contentType: string
}): void {

    // Convert base64 string to numbers array.
    const numArray = atob(options.byteArray).split('').map(c => c.charCodeAt(0));

    // Convert numbers array to Uint8Array object.
    const uint8Array = new Uint8Array(numArray);

    // Wrap it by Blob object.
    const blob = new Blob([uint8Array], { type: options.contentType });

    // Create "object URL" that is linked to the Blob object.
    const url = URL.createObjectURL(blob);

    // Invoke download helper function that implemented in 
    // the earlier section of this article.
    downloadFromUrl({ url: url, fileName: options.fileName });

    // At last, release unused resources.
    URL.revokeObjectURL(url);
}

//export function downloadfile(filename, contentType, content) {
//    // Create the URL
//    const file = new File([content], filename, { type: contentType });
//    const exportUrl = URL.createObjectURL(file);

//    // Create the <a> element and click on it
//    const a = document.createElement("a");
//    document.body.appendChild(a);
//    a.href = exportUrl;
//    a.download = filename;
//    a.target = "_self";
//    a.click();

//    // We don't need to keep the object URL, let's release the memory
//    // On older versions of Safari, it seems you need to comment this line...
//    URL.revokeObjectURL(exportUrl);
//}


//export function downloadfile1(option) {
//    // Create the URL
//    const file = new File([options.content], option.filename, { type: option.contentType });
//    const exportUrl = URL.createObjectURL(file);

//    // Create the <a> element and click on it
//    const a = document.createElement("a");
//    document.body.appendChild(a);
//    a.href = exportUrl;
//    a.download = option.filename;
//    a.target = "_self";
//    a.click();

//    // We don't need to keep the object URL, let's release the memory
//    // On older versions of Safari, it seems you need to comment this line...
//    URL.revokeObjectURL(exportUrl);
//}