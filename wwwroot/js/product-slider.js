(function() {
    function initProductSlider() {
        const productsDataElement = document.getElementById('productsData');
        if (!productsDataElement) return;

        const products = JSON.parse(productsDataElement.textContent);
        const lang = productsDataElement.getAttribute('data-lang');
        const isArabic = lang === 'ar';

        // Check if there's a productId in the URL to return to
        const urlParams = new URLSearchParams(window.location.search);
        const productIdFromUrl = urlParams.get('productId');
        let currentIndex = 0;
        
        // If productId is specified, find its index
        if (productIdFromUrl) {
            const foundIndex = products.findIndex(p => p.id == productIdFromUrl);
            if (foundIndex !== -1) {
                currentIndex = foundIndex;
            }
        }

        // Get elements
        const productName = document.getElementById('productName');
        const productSubtitle = document.getElementById('productSubtitle');
        const productDescription = document.getElementById('productDescription');
        const productImage = document.getElementById('productImage');
        const productBrochure = document.getElementById('productBrochure');
        const seeMoreBtn = document.getElementById('seeMoreBtn');

        const sliders = document.querySelectorAll('.dots-slider');
        
        // Get buttons and dots from both sliders (mobile and desktop)
        const allPrevBtns = [];
        const allNextBtns = [];
        const allDotGroups = [];
        
        sliders.forEach(slider => {
            const buttons = slider.querySelectorAll('button.btn-link');
            if (buttons.length >= 2) {
                allPrevBtns.push(buttons[0]);
                allNextBtns.push(buttons[1]);
            }
            
            // Get dots for this specific slider
            const dotsInSlider = slider.querySelectorAll('.horizontal-dots .dot');
            if (dotsInSlider.length > 0) {
                allDotGroups.push(dotsInSlider);
            }
        });

        function updateProduct() {
            const product = products[currentIndex];
            if (!product) return;

            productName.textContent = isArabic ? product.nameAr : product.nameEn;
            productSubtitle.textContent = isArabic ? product.subtitleAr : product.subtitleEn;
            productDescription.textContent = isArabic ? product.descriptionAr : product.descriptionEn;
            productImage.src = product.imagePath;
            productBrochure.href = product.brochurePath;
            productBrochure.textContent = `Download ${product.nameEn} brochure`;
            
            // Update See more button link
            if (seeMoreBtn) {
                seeMoreBtn.href = `/Product/Details/${product.id}`;
            }

            // Update subtitle style based on displayOrder
            if (product.displayOrder === 1) {
                productSubtitle.style.whiteSpace = 'normal';
            } else {
                productSubtitle.style.whiteSpace = 'pre-line';
            }

            // Update dots in all groups (mobile and desktop)
            allDotGroups.forEach(dotGroup => {
                dotGroup.forEach((dot, index) => {
                    dot.classList.toggle('active', index === currentIndex);
                });
            });
        }

        // Previous buttons (for both mobile and desktop)
        allPrevBtns.forEach(btn => {
            btn.addEventListener('click', () => {
                currentIndex = (currentIndex - 1 + products.length) % products.length;
                updateProduct();
            });
        });

        // Next buttons (for both mobile and desktop)
        allNextBtns.forEach(btn => {
            btn.addEventListener('click', () => {
                currentIndex = (currentIndex + 1) % products.length;
                updateProduct();
            });
        });

        // Dots navigation for all groups
        allDotGroups.forEach(dotGroup => {
            dotGroup.forEach((dot, index) => {
                dot.addEventListener('click', (e) => {
                    e.preventDefault();
                    currentIndex = index;
                    updateProduct();
                });
            });
        });

        // Initial update to display the correct product on page load
        updateProduct();
    }

    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initProductSlider);
    } else {
        initProductSlider();
    }
})();
