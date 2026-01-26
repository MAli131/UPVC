// Home2 Features Slider
(function() {
    // Wait for DOM to be ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initSlider);
    } else {
        initSlider();
    }

    function initSlider() {
        // Check if we're on Home2 page
        const featureIcon = document.getElementById('featureIcon');
        if (!featureIcon) return;

        // Get sections data from data attribute
        const sectionsData = document.getElementById('sectionsData');
        if (!sectionsData) return;

        const sections = JSON.parse(sectionsData.textContent);
        if (sections.length === 0) return;

        let currentIndex = 0;
        const isArabic = document.documentElement.lang === 'ar' || 
                        document.querySelector('html').getAttribute('dir') === 'rtl' ||
                        sectionsData.getAttribute('data-lang') === 'ar';

        function updateFeature() {
            const section = sections[currentIndex];
            const featureIcon = document.getElementById('featureIcon');
            const featureTitle = document.getElementById('featureTitle');
            const featureContent = document.getElementById('featureContent');

            if (featureIcon) featureIcon.src = section.imagePath;
            if (featureTitle) featureTitle.textContent = isArabic ? section.titleAr : section.titleEn;
            if (featureContent) featureContent.textContent = isArabic ? section.contentAr : section.contentEn;
        }

        // Previous button
        const prevBtn = document.getElementById('prevBtn');
        if (prevBtn) {
            prevBtn.addEventListener('click', () => {
                currentIndex = (currentIndex - 1 + sections.length) % sections.length;
                updateFeature();
            });
        }

        // Next button
        const nextBtn = document.getElementById('nextBtn');
        if (nextBtn) {
            nextBtn.addEventListener('click', () => {
                currentIndex = (currentIndex + 1) % sections.length;
                updateFeature();
            });
        }
    }
})();
