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
    
    // Create loading indicator
    function createLoadingIndicator() {
        const loader = document.createElement('div');
        loader.id = 'home-navigation-loader';
        loader.innerHTML = `
            <div class="loader-spinner"></div>
        `;
        
        // Add styles
        const style = document.createElement('style');
        style.textContent = `
            #home-navigation-loader {
                position: fixed;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                z-index: 9999;
                display: none;
            }
            
            #home-navigation-loader.active {
                display: block;
            }
            
            .loader-spinner {
                width: 50px;
                height: 50px;
                border: 4px solid rgba(255, 255, 255, 0.3);
                border-top: 4px solid #4DAC2A;
                border-radius: 50%;
                animation: spin 0.8s linear infinite;
            }
            
            @keyframes spin {
                0% { transform: rotate(0deg); }
                100% { transform: rotate(360deg); }
            }
            
            body.navigating {
                overflow: hidden;
            }
            
            body.navigating::after {
                content: '';
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background: rgba(0, 0, 0, 0.3);
                z-index: 9998;
                backdrop-filter: blur(2px);
            }
        `;
        
        document.head.appendChild(style);
        document.body.appendChild(loader);
        return loader;
    }
    
    // Show loading indicator
    function showLoading() {
        const loader = document.getElementById('home-navigation-loader') || createLoadingIndicator();
        loader.classList.add('active');
        document.body.classList.add('navigating');
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
            
            // Scroll down - go to next page
            if (delta > 0 && currentIndex < homePages.length - 1) {
                nextIndex = currentIndex + 1;
            }
            // Scroll up - go to previous page
            else if (delta < 0 && currentIndex > 0) {
                nextIndex = currentIndex - 1;
            }
            
            // Navigate to next page if changed
            if (nextIndex !== currentIndex) {
                isNavigating = true;
                lastScrollTime = Date.now();
                showLoading();
                
                // Small delay to ensure loading shows
                setTimeout(function() {
                    window.location.href = homePages[nextIndex].url;
                }, 100);
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
        
        // Swipe up (scroll down) - go to next page
        if (swipeDistance > 0 && currentIndex < homePages.length - 1) {
            nextIndex = currentIndex + 1;
        }
        // Swipe down (scroll up) - go to previous page
        else if (swipeDistance < 0 && currentIndex > 0) {
            nextIndex = currentIndex - 1;
        }
        
        // Navigate to next page if changed
        if (nextIndex !== currentIndex) {
            isNavigating = true;
            lastScrollTime = Date.now();
            showLoading();
            
            setTimeout(function() {
                window.location.href = homePages[nextIndex].url;
            }, 100);
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
                
                // Down arrow or Page Down
                if ((event.key === 'ArrowDown' || event.key === 'PageDown') && currentIndex < homePages.length - 1) {
                    event.preventDefault();
                    nextIndex = currentIndex + 1;
                }
                // Up arrow or Page Up
                else if ((event.key === 'ArrowUp' || event.key === 'PageUp') && currentIndex > 0) {
                    event.preventDefault();
                    nextIndex = currentIndex - 1;
                }
                
                // Navigate if changed
                if (nextIndex !== currentIndex) {
                    isNavigating = true;
                    lastScrollTime = Date.now();
                    showLoading();
                    
                    setTimeout(function() {
                        window.location.href = homePages[nextIndex].url;
                    }, 100);
                }
            });
        }
    }
    
    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
