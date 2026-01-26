// Home pages navigation using mouse scroll
(function() {
    'use strict';
    
    // Define the home pages in order
    const homePages = [
        { action: 'Index', url: '/Home/Index' },
        { action: 'Home2', url: '/Home/Home2' },
        { action: 'Home3', url: '/Home/Home3' },
        { action: 'Home4', url: '/Home/Home4' }
    ];
    
    // Create smooth page transition effect
    function createPageTransition() {
        // Add styles for smooth page flip effect
        const style = document.createElement('style');
        style.textContent = `
            html, body {
                background: #ffffff !important;
            }
            
            body.page-transitioning {
                overflow: hidden !important;
                background: #ffffff !important;
            }
            
            body.page-transitioning .hero-section {
                position: relative;
                z-index: 10;
                background: inherit;
            }
            
            /* Flip page up (going to next page) */
            body.page-transitioning.flip-up .hero-section {
                animation: pageSlideUp 0.5s cubic-bezier(0.4, 0, 0.2, 1) forwards;
            }
            
            @keyframes pageSlideUp {
                0% {
                    transform: translateY(0);
                    opacity: 1;
                }
                100% {
                    transform: translateY(-100%);
                    opacity: 0;
                }
            }
            
            /* Flip page down (going to previous page) */
            body.page-transitioning.flip-down .hero-section {
                animation: pageSlideDown 0.5s cubic-bezier(0.4, 0, 0.2, 1) forwards;
            }
            
            @keyframes pageSlideDown {
                0% {
                    transform: translateY(0);
                    opacity: 1;
                }
                100% {
                    transform: translateY(100%);
                    opacity: 0;
                }
            }
        `;
        
        document.head.appendChild(style);
    }
    
    // Apply transition effect and navigate
    function applyTransitionAndNavigate(url, direction) {
        // Create transition styles if not exists
        if (!document.querySelector('style[data-page-transition]')) {
            const style = createPageTransition();
            if (style) style.setAttribute('data-page-transition', 'true');
        }
        
        // Add transition class with direction
        document.body.classList.add('page-transitioning');
        document.body.classList.add(direction === 'down' ? 'flip-up' : 'flip-down');
        
        // Navigate after animation completes
        setTimeout(function() {
            window.location.href = url;
        }, 480); // Match animation duration
    }
    
    // Get current page index
    function getCurrentPageIndex() {
        const path = window.location.pathname.toLowerCase();
        
        if (path.includes('/home/home2')) return 1;
        if (path.includes('/home/home3')) return 2;
        if (path.includes('/home/home4')) return 3;
        if (path.includes('/home') || path === '/') return 0;
        
        return -1; // Not a home page
    }
    
    let isNavigating = false;
    let scrollTimeout;
    let lastScrollTime = 0;
    const SCROLL_COOLDOWN = 1000; // 1 second cooldown between navigations
    
    // Touch handling variables
    let touchStartY = 0;
    let touchEndY = 0;
    const SWIPE_THRESHOLD = 50; // Minimum swipe distance in pixels
    
    // Handle scroll navigation
    function handleScroll(event) {
        // Only handle if we're on a home page
        const currentIndex = getCurrentPageIndex();
        if (currentIndex === -1 || isNavigating) {
            event.preventDefault();
            return;
        }
        
        // Check cooldown period
        const now = Date.now();
        if (now - lastScrollTime < SCROLL_COOLDOWN) {
            event.preventDefault();
            return;
        }
        
        // Prevent default scroll behavior
        event.preventDefault();
        
        // Clear previous timeout
        if (scrollTimeout) {
            clearTimeout(scrollTimeout);
        }
        
        // Debounce scroll events
        scrollTimeout = setTimeout(function() {
            // Double check we're not already navigating
            if (isNavigating) return;
            
            const delta = Math.sign(event.deltaY);
            let nextIndex = currentIndex;
            let direction = null;
            
            // Scroll down - go to next page
            if (delta > 0 && currentIndex < homePages.length - 1) {
                nextIndex = currentIndex + 1;
                direction = 'down';
            }
            // Scroll up - go to previous page
            else if (delta < 0 && currentIndex > 0) {
                nextIndex = currentIndex - 1;
                direction = 'up';
            }
            
            // Navigate to next page if changed
            if (nextIndex !== currentIndex) {
                isNavigating = true;
                lastScrollTime = Date.now();
                applyTransitionAndNavigate(homePages[nextIndex].url, direction);
            }
        }, 200); // Increased debounce delay
    }
    
    // Handle touch start
    function handleTouchStart(event) {
        const currentIndex = getCurrentPageIndex();
        if (currentIndex === -1 || isNavigating) return;
        
        touchStartY = event.touches[0].clientY;
    }
    
    // Handle touch move
    function handleTouchMove(event) {
        const currentIndex = getCurrentPageIndex();
        if (currentIndex === -1 || isNavigating) return;
        
        // Prevent default scrolling during navigation
        if (Math.abs(event.touches[0].clientY - touchStartY) > SWIPE_THRESHOLD) {
            event.preventDefault();
        }
    }
    
    // Handle touch end
    function handleTouchEnd(event) {
        const currentIndex = getCurrentPageIndex();
        if (currentIndex === -1 || isNavigating) return;
        
        // Check cooldown period
        const now = Date.now();
        if (now - lastScrollTime < SCROLL_COOLDOWN) {
            return;
        }
        
        touchEndY = event.changedTouches[0].clientY;
        const swipeDistance = touchStartY - touchEndY;
        
        // Only navigate if swipe is significant enough
        if (Math.abs(swipeDistance) < SWIPE_THRESHOLD) {
            return;
        }
        
        let nextIndex = currentIndex;
        let direction = null;
        
        // Swipe up (scroll down) - go to next page
        if (swipeDistance > 0 && currentIndex < homePages.length - 1) {
            nextIndex = currentIndex + 1;
            direction = 'down';
        }
        // Swipe down (scroll up) - go to previous page
        else if (swipeDistance < 0 && currentIndex > 0) {
            nextIndex = currentIndex - 1;
            direction = 'up';
        }
        
        // Navigate to next page if changed
        if (nextIndex !== currentIndex) {
            isNavigating = true;
            lastScrollTime = Date.now();
            applyTransitionAndNavigate(homePages[nextIndex].url, direction);
        }
    }
    
    // Initialize scroll navigation
    function init() {
        // Check if we're on a home page
        if (getCurrentPageIndex() !== -1) {
            // Prevent navigation immediately after page load
            lastScrollTime = Date.now();
            
            // Add wheel event listener with passive: false to allow preventDefault
            window.addEventListener('wheel', handleScroll, { passive: false });
            
            // Add touch event listeners for mobile support
            window.addEventListener('touchstart', handleTouchStart, { passive: true });
            window.addEventListener('touchmove', handleTouchMove, { passive: false });
            window.addEventListener('touchend', handleTouchEnd, { passive: true });
            
            // Also handle arrow keys for additional navigation
            window.addEventListener('keydown', function(event) {
                const currentIndex = getCurrentPageIndex();
                if (currentIndex === -1 || isNavigating) return;
                
                // Check cooldown period
                const now = Date.now();
                if (now - lastScrollTime < SCROLL_COOLDOWN) {
                    return;
                }
                
                let nextIndex = currentIndex;
                let direction = null;
                
                // Down arrow or Page Down
                if ((event.key === 'ArrowDown' || event.key === 'PageDown') && currentIndex < homePages.length - 1) {
                    event.preventDefault();
                    nextIndex = currentIndex + 1;
                    direction = 'down';
                }
                // Up arrow or Page Up
                else if ((event.key === 'ArrowUp' || event.key === 'PageUp') && currentIndex > 0) {
                    event.preventDefault();
                    nextIndex = currentIndex - 1;
                    direction = 'up';
                }
                
                // Navigate if changed
                if (nextIndex !== currentIndex) {
                    isNavigating = true;
                    lastScrollTime = Date.now();
                    applyTransitionAndNavigate(homePages[nextIndex].url, direction);
                }
            });
            
            // Handle hero arrow button click
            const heroArrowBtn = document.getElementById('heroArrowBtn');
            if (heroArrowBtn) {
                heroArrowBtn.addEventListener('click', function(event) {
                    event.preventDefault();
                    
                    // Check if already navigating
                    if (isNavigating) return;
                    
                    const nextPageUrl = this.getAttribute('data-next-page');
                    if (nextPageUrl) {
                        isNavigating = true;
                        lastScrollTime = Date.now();
                        applyTransitionAndNavigate(nextPageUrl, 'down');
                    }
                });
            }
        }
    }
    
    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
