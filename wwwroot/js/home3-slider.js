// Home3 Sections Slider
(function() {
    // Wait for DOM to be ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initSlider);
    } else {
        initSlider();
    }

    function initSlider() {
        const sliderContainer = document.querySelector('.home3 .hero-items-container');
        if (!sliderContainer) return;

        const items = sliderContainer.querySelectorAll('.hero-items');
        if (items.length === 0) return;

        let currentIndex = 0;

        // Hide all items except first
        function showSlide(index) {
            items.forEach((item, i) => {
                if (i === index) {
                    item.style.display = 'block';
                } else {
                    item.style.display = 'none';
                }
            });
            updateDots();
        }

        // Create navigation buttons
        const navContainer = document.createElement('div');
        navContainer.className = 'slider-navigation d-md-none';
        navContainer.innerHTML = `
            <button type="button" class="slider-btn prev-btn" aria-label="Previous">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                    <path d="M15 18L9 12L15 6" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
            </button>
            <div class="slider-dots"></div>
            <button type="button" class="slider-btn next-btn" aria-label="Next">
                <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                    <path d="M9 18L15 12L9 6" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                </svg>
            </button>
        `;

        sliderContainer.parentElement.appendChild(navContainer);

        // Create dots
        const dotsContainer = navContainer.querySelector('.slider-dots');
        items.forEach((_, index) => {
            const dot = document.createElement('span');
            dot.className = 'slider-dot';
            if (index === 0) dot.classList.add('active');
            dot.addEventListener('click', () => {
                currentIndex = index;
                showSlide(currentIndex);
            });
            dotsContainer.appendChild(dot);
        });

        function updateDots() {
            const dots = dotsContainer.querySelectorAll('.slider-dot');
            dots.forEach((dot, i) => {
                if (i === currentIndex) {
                    dot.classList.add('active');
                } else {
                    dot.classList.remove('active');
                }
            });
        }

        // Previous button
        const prevBtn = navContainer.querySelector('.prev-btn');
        prevBtn.addEventListener('click', () => {
            currentIndex = (currentIndex - 1 + items.length) % items.length;
            showSlide(currentIndex);
        });

        // Next button
        const nextBtn = navContainer.querySelector('.next-btn');
        nextBtn.addEventListener('click', () => {
            currentIndex = (currentIndex + 1) % items.length;
            showSlide(currentIndex);
        });

        // Touch support for swipe
        let touchStartX = 0;
        let touchEndX = 0;

        sliderContainer.addEventListener('touchstart', (e) => {
            touchStartX = e.changedTouches[0].screenX;
        });

        sliderContainer.addEventListener('touchend', (e) => {
            touchEndX = e.changedTouches[0].screenX;
            handleSwipe();
        });

        function handleSwipe() {
            const swipeThreshold = 50;
            if (touchStartX - touchEndX > swipeThreshold) {
                // Swipe left - next
                currentIndex = (currentIndex + 1) % items.length;
                showSlide(currentIndex);
            } else if (touchEndX - touchStartX > swipeThreshold) {
                // Swipe right - previous
                currentIndex = (currentIndex - 1 + items.length) % items.length;
                showSlide(currentIndex);
            }
        }

        // Show first slide only on mobile
        if (window.innerWidth < 768) {
            showSlide(0);
        } else {
            // Show all items on desktop
            items.forEach(item => item.style.display = 'block');
        }

        // Handle window resize
        window.addEventListener('resize', () => {
            if (window.innerWidth < 768) {
                showSlide(currentIndex);
                navContainer.style.display = 'flex';
            } else {
                items.forEach(item => item.style.display = 'block');
                navContainer.style.display = 'none';
            }
        });
    }
})();
