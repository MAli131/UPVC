(function() {
    function initProductSlider() {
        const productsDataElement = document.getElementById('productsData');
        if (!productsDataElement) return;

        const products = JSON.parse(productsDataElement.textContent);
        const lang = productsDataElement.getAttribute('data-lang');
        const isArabic = lang === 'ar';

        let currentIndex = 0;

        // Get elements
        const productName = document.getElementById('productName');
        const productSubtitle = document.getElementById('productSubtitle');
        const productDescription = document.getElementById('productDescription');
        const productImage = document.getElementById('productImage');
        const productBrochure = document.getElementById('productBrochure');
        const seeMoreBtn = document.getElementById('seeMoreBtn');

        const dots = document.querySelectorAll('.horizontal-dots .dot');
        const prevBtn = document.querySelector('.dots-slider .btn-link:first-of-type');
        const nextBtn = document.querySelector('.dots-slider .btn-link:last-of-type');

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

            // Update dots
            dots.forEach((dot, index) => {
                dot.classList.toggle('active', index === currentIndex);
            });
        }

        // Previous button
        prevBtn?.addEventListener('click', () => {
            currentIndex = (currentIndex - 1 + products.length) % products.length;
            updateProduct();
        });

        // Next button
        nextBtn?.addEventListener('click', () => {
            currentIndex = (currentIndex + 1) % products.length;
            updateProduct();
        });

        // Dots navigation
        dots.forEach((dot, index) => {
            dot.addEventListener('click', (e) => {
                e.preventDefault();
                currentIndex = index;
                updateProduct();
            });
        });
    }

    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initProductSlider);
    } else {
        initProductSlider();
    }
})();
