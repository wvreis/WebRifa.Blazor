function exportToPDF(contentId, fileName) {
    return new Promise(() => {
        const element = document.getElementById(contentId);
        html2pdf()
            .set({
                margin: 0,
                filename: `${fileName}.pdf`,
                image: {
                    type: "jpeg",
                    quality: 0.9,
                },
            })
            .from(element)
            .save();
    });
}