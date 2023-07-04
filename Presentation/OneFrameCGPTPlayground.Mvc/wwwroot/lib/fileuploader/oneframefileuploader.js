"use strict";
// Class definition

var component = null;

var FileUpload = function (selector, options) {
    Dropzone.autoDiscover = false;
    // customize options, dict, functions
    component = new Dropzone(selector, {
        /**
         * Has to be specified on elements other than form (or when the form
         * doesn't have an `action` attribute). You can also
         * provide a function that will be called with `files` and
         * must return the url
         */
        url: options.url,

        /**
         * Can be changed to `"put"` if necessary. You can also provide a function
         * that will be called with `files` and must return the method.
         */
        method: options.method,

        /**
         * Will be set on the XHRequest.
         */
        withCredentials: options.withCredentials,

        /**
         * The timeout for the XHR requests in milliseconds
         */
        timeout: options.timeout,

        /**
         * How many file uploads to process in parallel (See the
         * Enqueuing file uploads* documentation section for more info)
         */
        parallelUploads: options.parallelUploads,

        /**
         * Whether to send multiple files in one request. If
         * this it set to true, then the fallback file input element will
         * have the `multiple` attribute as well. This option will
         * also trigger additional events (like `processingmultiple`). See the events
         * documentation section for more information.
         */
        uploadMultiple: options.uploadMultiple,

        /**
         * Whether you want files to be uploaded in chunks to your server. This can't be
         * used in combination with `uploadMultiple`.
         *
         * See [chunksUploaded](#config-chunksUploaded) for the callback to finalise an upload.
         */
        chunking: options.chunking,

        /**
         * If `chunking` is enabled, this defines whether **every** file should be chunked,
         * even if the file size is below chunkSize. This means, that the additional chunk
         * form data will be submitted and the `chunksUploaded` callback will be invoked.
         */
        forceChunking: options.forceChunking,

        /**
         * If `chunking` is `true`, then this defines the chunk size in bytes.
         */
        chunkSize: options.chunkSize,

        /**
         * If `true`, the individual chunks of a file are being uploaded simultaneously.
         */
        parallelChunkUploads: options.parallelChunkUploads,

        /**
         * Whether a chunk should be retried if it fails.
         */
        retryChunks: options.retryChunks,

        /**
         * If `retryChunks` is true, how many times should it be retried.
         */
        retryChunksLimit: options.retryChunksLimit,

        /**
         * If not `null` defines how many files this Dropzone handles. If it exceeds,
         * the event `maxfilesexceeded` will be called. The dropzone element gets the
         * class `dz-max-files-reached` accordingly so you can provide visual feedback.
         */
        maxFilesize: options.maxFilesize,

        /**
         * The name of the file param that gets transferred.
         * **NOTE**: If you have the option  `uploadMultiple` set to `true`, then
         * Dropzone will append `[]` to the name.
         */
        paramName: options.paramName,

        /**
         * Whether thumbnails for images should be generated
         */
        createImageThumbnails: options.createImageThumbnails,

        /**
         * In MB. When the filename exceeds this limit, the thumbnail will not be generated.
         */
        maxThumbnailFilesize: options.maxThumbnailFilesize,

        /**
         * If `null`, the ratio of the image will be used to calculate it.
         */
        thumbnailWidth: options.thumbnailWidth,

        /**
         * The same as `thumbnailWidth`. If both are null, images will not be resized.
         */
        thumbnailHeight: options.thumbnailHeight,

        /**
         * How the images should be scaled down in case both, `thumbnailWidth` and `thumbnailHeight` are provided.
         * Can be either `contain` or `crop`.
         */
        thumbnailMethod: options.thumbnailMethod,

        /**
         * If set, images will be resized to these dimensions before being **uploaded**.
         * If only one, `resizeWidth` **or** `resizeHeight` is provided, the original aspect
         * ratio of the file will be preserved.
         *
         * The `options.transformFile` function uses these options, so if the `transformFile` function
         * is overridden, these options don't do anything.
         */
        resizeWidth: options.resizeWidth,

        /**
         * See `resizeWidth`.
         */
        resizeHeight: options.resizeHeight,

        /**
         * The mime type of the resized image (before it gets uploaded to the server).
         * If `null` the original mime type will be used. To force jpeg, for example, use `image/jpeg`.
         * See `resizeWidth` for more information.
         */
        resizeMimeType: options.resizeMimeType,

        /**
         * The quality of the resized images. See `resizeWidth`.
         */
        resizeQuality: options.resizeQuality,

        /**
         * How the images should be scaled down in case both, `resizeWidth` and `resizeHeight` are provided.
         * Can be either `contain` or `crop`.
         */
        resizeMethod: options.resizeMethod,

        /**
         * The base that is used to calculate the filesize. You can change this to
         * 1024 if you would rather display kibibytes, mebibytes, etc...
         * 1024 is technically incorrect, because `1024 bytes` are `1 kibibyte` not `1 kilobyte`.
         * You can change this to `1024` if you don't care about validity.
         */
        filesizeBase: options.filesizeBase,

        /**
         * Can be used to limit the maximum number of files that will be handled by this Dropzone
         */
        maxFiles: options.maxFiles,

        /**
         * An optional object to send additional headers to the server. Eg:
         * `{ "My-Awesome-Header": "header value" }`
         */
        headers: options.headers,

        /**
         * If `true`, the dropzone element itself will be clickable, if `false`
         * nothing will be clickable.
         *
         * You can also pass an HTML element, a CSS selector (for multiple elements)
         * or an array of those. In that case, all of those elements will trigger an
         * upload when clicked.
         */
        clickable: options.clickable,

        /**
         * Whether hidden files in directories should be ignored.
         */
        ignoreHiddenFiles: options.ignoreHiddenFiles,

        /**
         * The default implementation of `accept` checks the file's mime type or
         * extension against this list. This is a comma separated list of mime
         * types or file extensions.
         *
         * Eg.: `image/*,application/pdf,.psd`
         *
         * If the Dropzone is `clickable` this option will also be used as accept
         * parameter on the hidden file input as well.
         */
        acceptedFiles: options.acceptedFiles,

        /**
         * **Deprecated!**
         * Use acceptedFiles instead.
         */
        acceptedMimeTypes: options.acceptedMimeTypes,

        /**
         * If false, files will be added to the queue but the queue will not be
         * processed automatically.
         * This can be useful if you need some additional user input before sending
         * files (or if you want want all files sent at once).
         * If you're ready to send the file simply call `myDropzone.processQueue()`.
         *
         * See the [enqueuing file uploads](#enqueuing-file-uploads) documentation
         * section for more information.
         */
        autoProcessQueue: options.autoProcessQueue,

        /**
         * If false, files added to the dropzone will not be queued by default.
         * You'll have to call `enqueueFile(file)` manually.
         */
        autoQueue: options.autoQueue,

        /**
         * If `true`, this will add a link to every file preview to remove or cancel (if
         * already uploading) the file. The `dictCancelUpload`, `dictCancelUploadConfirmation`
         * and `dictRemoveFile` options are used for the wording.
         */
        addRemoveLinks: options.addRemoveLinks,

        /**
         * Defines where to display the file previews – if `null` the
         * Dropzone element itself is used. Can be a plain `HTMLElement` or a CSS
         * selector. The element should have the `dropzone-previews` class so
         * the previews are displayed properly.
         */
        previewsContainer: options.previewsContainer,

        /**
         * This is the element the hidden input field (which is used when clicking on the
         * dropzone to trigger file selection) will be appended to. This might
         * be important in case you use frameworks to switch the content of your page.
         *
         * Can be a selector string, or an element directly.
         */
        hiddenInputContainer: options.hiddenInputContainer,

        /**
         * If null, no capture type will be specified
         * If camera, mobile devices will skip the file selection and choose camera
         * If microphone, mobile devices will skip the file selection and choose the microphone
         * If camcorder, mobile devices will skip the file selection and choose the camera in video mode
         * On apple devices multiple must be set to false.  AcceptedFiles may need to
         * be set to an appropriate mime type (e.g. "image/*", "audio/*", or "video/*").
         */
        capture: options.capture,

        /**
         * **Deprecated**. Use `renameFile` instead.
         */
        renameFilename: options.renameFilename,

        /**
         * A function that is invoked before the file is uploaded to the server and renames the file.
         * This function gets the `File` as argument and can use the `file.name`. The actual name of the
         * file that gets used during the upload can be accessed through `file.upload.filename`.
         */
        renameFile: options.renameFile,

        /**
         * If `true` the fallback will be forced. This is very useful to test your server
         * implementations first and make sure that everything works as
         * expected without dropzone if you experience problems, and to test
         * how your fallbacks will look.
         */
        forceFallback: options.forceFallback,

        /**
          * The text used before any files are dropped.
          */
        dictDefaultMessage: L("DropFilesHereOrClickToUpload"),

        /**
         * The text that replaces the default message text it the browser is not supported.
         */
        dictFallbackMessage: L("YourBrowserDoesNotSupportDragDropFileUploads"),

        /**
         * The text that will be added before the fallback form.
         * If you provide a  fallback element yourself, or if this option is `null` this will
         * be ignored.
         */
        dictFallbackText: L("PleaseUseTheFallbackFormBelowToUploadYourFilesLikeInTheOldenDays"),

        /**
         * If the filesize is too big.
         * `{{filesize}}` and `{{maxFilesize}}` will be replaced with the respective configuration values.
         */
        dictFileTooBig: L("FileIsTooBig"),

        /**
         * If the file doesn't match the file type.
         */
        dictInvalidFileType: L("YouCantUploadFilesOfThisType"),

        /**
         * If the server response was invalid.
         * `{{statusCode}}` will be replaced with the servers status code.
         */
        dictResponseError: L("ServerrRespondedWithCode"),

        /**
         * If `addRemoveLinks` is true, the text to be used for the cancel upload link.
         */
        dictCancelUpload: L("CancelUpload"),

        /**
         * The text that is displayed if an upload was manually canceled
         */
        dictUploadCanceled: L("UploadCanceled"),

        /**
         * If `addRemoveLinks` is true, the text to be used for confirmation when cancelling upload.
         */
        dictCancelUploadConfirmation: L("AreYouSureYouWantToCancelThisUpload"),

        /**
         * If `addRemoveLinks` is true, the text to be used to remove a file.
         */
        dictRemoveFile: L("RemoveFile"),

        /**
         * If this is not null, then the user will be prompted before removing a file.
         */
        dictRemoveFileConfirmation: null,

        /**
         * Displayed if `maxFiles` is st and exceeded.
         * The string `{{maxFiles}}` will be replaced by the configuration value.
         */
        dictMaxFilesExceeded: L("YouCanNotUploadAnyMoreFiles"),

        /**
         * Allows you to translate the different units. Starting with `tb` for terabytes and going down to
         * `b` for bytes.
         */
        dictFileSizeUnits: { tb: "TB", gb: "GB", mb: "MB", kb: "KB", b: "b" },

        /**
         * Called when dropzone initialized
         * You can add event listeners here
         */
        init: options.init === undefined ? function () { } : options.init,

        /**
         * Can be an **object** of additional parameters to transfer to the server, **or** a `Function`
         * that gets invoked with the `files`, `xhr` and, if it's a chunked upload, `chunk` arguments. In case
         * of a function, this needs to return a map.
         *
         * The default implementation does nothing for normal uploads, but adds relevant information for
         * chunked uploads.
         *
         * This is the same as adding hidden input fields in the form element.
         */
        params: options.params === undefined ? function params(files, xhr, chunk) {
            if (chunk) {
                return {
                    dzuuid: chunk.file.upload.uuid,
                    dzchunkindex: chunk.index,
                    dztotalfilesize: chunk.file.size,
                    dzchunksize: this.options.chunkSize,
                    dztotalchunkcount: chunk.file.upload.totalChunkCount,
                    dzchunkbyteoffset: chunk.index * this.options.chunkSize
                };
            }
        } : options.params,

        /**
         * A function that gets file
         * and a `done` function as parameters.
         *
         * If the done function is invoked without arguments, the file is "accepted" and will
         * be processed. If you pass an error message, the file is rejected, and the error
         * message will be displayed.
         * This function will not be called if the file is too big or doesn't match the mime types.
         */
        accept: options.accept === undefined ? function accept(file, done) {
            return done();
        } : options.accept,

        /**
         * The callback that will be invoked when all chunks have been uploaded for a file.
         * It gets the file for which the chunks have been uploaded as the first parameter,
         * and the `done` function as second. `done()` needs to be invoked when everything
         * needed to finish the upload process is done.
         */
        chunksUploaded: options.chunksUploaded === undefined ? function chunksUploaded(file, done) {
            done();
        } : options.chunksUploaded,

        /**
         * Gets called when the browser is not supported.
         * The default implementation shows the fallback input field and adds
         * a text.
         */
        fallback: options.fallback === undefined ? function fallback() {
            // This code should pass in IE7... :(
            var messageElement = void 0;
            this.element.className = this.element.className + " dz-browser-not-supported";

            for (var child in this.element.getElementsByTagName("div")) {
                if (/(^| )dz-message($| )/.test(child.className)) {
                    messageElement = child;
                    child.className = "dz-message"; // Removes the 'dz-default' class
                    break;
                }
            }
            if (!messageElement) {
                messageElement = Dropzone.createElement("<div class=\"dz-message\"><span></span></div>");
                this.element.appendChild(messageElement);
            }

            var span = messageElement.getElementsByTagName("span")[0];
            if (span) {
                if (span.textContent != null) {
                    span.textContent = this.options.dictFallbackMessage;
                } else if (span.innerText != null) {
                    span.innerText = this.options.dictFallbackMessage;
                }
            }

            return this.element.appendChild(this.getFallbackForm());
        } : options.fallback,

        /**
         * Gets called to calculate the thumbnail dimensions.
         *
         * It gets `file`, `width` and `height` (both may be `null`) as parameters and must return an object containing:
         *
         *  - `srcWidth` & `srcHeight` (required)
         *  - `trgWidth` & `trgHeight` (required)
         *  - `srcX` & `srcY` (optional, default `0`)
         *  - `trgX` & `trgY` (optional, default `0`)
         *
         * Those values are going to be used by `ctx.drawImage()`.
         */
        resize: options.resize === undefined ? function resize(file, width, height, resizeMethod) {
            var info = {
                srcX: 0,
                srcY: 0,
                srcWidth: file.width,
                srcHeight: file.height
            };

            var srcRatio = file.width / file.height;

            // Automatically calculate dimensions if not specified
            if (width == null && height == null) {
                width = info.srcWidth;
                height = info.srcHeight;
            } else if (width == null) {
                width = height * srcRatio;
            } else if (height == null) {
                height = width / srcRatio;
            }

            // Make sure images aren't upscaled
            width = Math.min(width, info.srcWidth);
            height = Math.min(height, info.srcHeight);

            var trgRatio = width / height;

            if (info.srcWidth > width || info.srcHeight > height) {
                // Image is bigger and needs rescaling
                if (resizeMethod === 'crop') {
                    if (srcRatio > trgRatio) {
                        info.srcHeight = file.height;
                        info.srcWidth = info.srcHeight * trgRatio;
                    } else {
                        info.srcWidth = file.width;
                        info.srcHeight = info.srcWidth / trgRatio;
                    }
                } else if (resizeMethod === 'contain') {
                    // Method 'contain'
                    if (srcRatio > trgRatio) {
                        height = width / srcRatio;
                    } else {
                        width = height * srcRatio;
                    }
                } else {
                    throw new Error(L("Unknown resizeMethod {{resizeMethod}}"));
                }
            }

            info.srcX = (file.width - info.srcWidth) / 2;
            info.srcY = (file.height - info.srcHeight) / 2;

            info.trgWidth = width;
            info.trgHeight = height;

            return info;
        } : options.resize,

        /**
         * Can be used to transform the file (for example, resize an image if necessary).
         *
         * The default implementation uses `resizeWidth` and `resizeHeight` (if provided) and resizes
         * images according to those dimensions.
         *
         * Gets the `file` as the first parameter, and a `done()` function as the second, that needs
         * to be invoked with the file when the transformation is done.
         */
        transformFile: options.transformFile === undefined ? function transformFile(file, done) {
            if ((this.options.resizeWidth || this.options.resizeHeight) && file.type.match(/image.*/)) {
                return this.resizeImage(file, this.options.resizeWidth, this.options.resizeHeight, this.options.resizeMethod, done);
            } else {
                return done(file);
            }
        } : options.transformFile,

        /**
         * A string that contains the template used for each dropped
         * file. Change it to fulfill your needs but make sure to properly
         * provide all elements.
         *
         * If you want to use an actual HTML element instead of providing a String
         * as a config option, you could create a div with the id `tpl`,
         * put the template inside it and provide the element like this:
         *
         *     document
         *       .querySelector('#tpl')
         *       .innerHTML
         *
         */
        previewTemplate: options.previewTemplate == undefined ? "<div class=\"dz-preview dz-file-preview\">\n  <div class=\"dz-image\"><img data-dz-thumbnail /></div>\n  <div class=\"dz-details\">\n    <div class=\"dz-size\"><span data-dz-size></span></div>\n    <div class=\"dz-filename\"><span data-dz-name></span></div>\n  </div>\n  <div class=\"dz-progress\"><span class=\"dz-upload\" data-dz-uploadprogress></span></div>\n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n  <div class=\"dz-success-mark\">\n    <svg width=\"54px\" height=\"54px\" viewBox=\"0 0 54 54\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:sketch=\"http://www.bohemiancoding.com/sketch/ns\">\n      <title>Check</title>\n      <defs></defs>\n      <g id=\"Page-1\" stroke=\"none\" stroke-width=\"1\" fill=\"none\" fill-rule=\"evenodd\" sketch:type=\"MSPage\">\n        <path d=\"M23.5,31.8431458 L17.5852419,25.9283877 C16.0248253,24.3679711 13.4910294,24.366835 11.9289322,25.9289322 C10.3700136,27.4878508 10.3665912,30.0234455 11.9283877,31.5852419 L20.4147581,40.0716123 C20.5133999,40.1702541 20.6159315,40.2626649 20.7218615,40.3488435 C22.2835669,41.8725651 24.794234,41.8626202 26.3461564,40.3106978 L43.3106978,23.3461564 C44.8771021,21.7797521 44.8758057,19.2483887 43.3137085,17.6862915 C41.7547899,16.1273729 39.2176035,16.1255422 37.6538436,17.6893022 L23.5,31.8431458 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z\" id=\"Oval-2\" stroke-opacity=\"0.198794158\" stroke=\"#747474\" fill-opacity=\"0.816519475\" fill=\"#FFFFFF\" sketch:type=\"MSShapeGroup\"></path>\n      </g>\n    </svg>\n  </div>\n  <div class=\"dz-error-mark\">\n    <svg width=\"54px\" height=\"54px\" viewBox=\"0 0 54 54\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:sketch=\"http://www.bohemiancoding.com/sketch/ns\">\n      <title>Error</title>\n      <defs></defs>\n      <g id=\"Page-1\" stroke=\"none\" stroke-width=\"1\" fill=\"none\" fill-rule=\"evenodd\" sketch:type=\"MSPage\">\n        <g id=\"Check-+-Oval-2\" sketch:type=\"MSLayerGroup\" stroke=\"#747474\" stroke-opacity=\"0.198794158\" fill=\"#FFFFFF\" fill-opacity=\"0.816519475\">\n          <path d=\"M32.6568542,29 L38.3106978,23.3461564 C39.8771021,21.7797521 39.8758057,19.2483887 38.3137085,17.6862915 C36.7547899,16.1273729 34.2176035,16.1255422 32.6538436,17.6893022 L27,23.3431458 L21.3461564,17.6893022 C19.7823965,16.1255422 17.2452101,16.1273729 15.6862915,17.6862915 C14.1241943,19.2483887 14.1228979,21.7797521 15.6893022,23.3461564 L21.3431458,29 L15.6893022,34.6538436 C14.1228979,36.2202479 14.1241943,38.7516113 15.6862915,40.3137085 C17.2452101,41.8726271 19.7823965,41.8744578 21.3461564,40.3106978 L27,34.6568542 L32.6538436,40.3106978 C34.2176035,41.8744578 36.7547899,41.8726271 38.3137085,40.3137085 C39.8758057,38.7516113 39.8771021,36.2202479 38.3106978,34.6538436 L32.6568542,29 Z M27,53 C41.3594035,53 53,41.3594035 53,27 C53,12.6405965 41.3594035,1 27,1 C12.6405965,1 1,12.6405965 1,27 C1,41.3594035 12.6405965,53 27,53 Z\" id=\"Oval-2\" sketch:type=\"MSShapeGroup\"></path>\n        </g>\n      </g>\n    </svg>\n  </div>\n</div>"
            : options.previewTemplate,
    });

    return component;
};

var RemoveFiles = function () {
    component.removeAllFiles(true);
    $('div.dz-image').remove();
}