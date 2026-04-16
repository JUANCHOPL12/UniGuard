window.iniciarCamara = async (videoElementId) => {
    try {
        const video = document.getElementById(videoElementId);
        const stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
        video.srcObject = stream;
        video.play();
        return true;
    } catch (err) {
        console.error("Error al acceder a la cámara:", err);
        return false;
    }
};

window.capturarFoto = (videoElementId, canvasElementId) => {
    const video = document.getElementById(videoElementId);
    const canvas = document.getElementById(canvasElementId);
    const context = canvas.getContext('2d');

    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    context.drawImage(video, 0, 0, canvas.width, canvas.height);

    return canvas.toDataURL('image/png');
};