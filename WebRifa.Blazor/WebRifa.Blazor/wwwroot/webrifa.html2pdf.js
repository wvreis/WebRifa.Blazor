function exportToPDF(contentId, fileName) {
    return new Promise(() => {
        const element = document.getElementById(contentId);
        html2pdf()
            .set({
                margin: 0,
                filename: `${fileName}.pdf`,
            })
            .from(element)
            .save();
    });
}