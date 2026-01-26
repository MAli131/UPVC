// Auto-play home pages slider
(function() {
    let autoPlayInterval;
    const pages = ['Index', 'Home2', 'Home3', 'Home4'];
    
    // Get current page index from active dot
    function getCurrentPageIndex() {
        const activeDot = document.querySelector('.vertical-dots .dot.active');
        if (!activeDot) return 0;
        
        const href = activeDot.getAttribute('href') || activeDot.getAttribute('asp-action');
        if (href && href.includes('Home2')) return 1;
        if (href && href.includes('Home3')) return 2;
        if (href && href.includes('Home4')) return 3;
        return 0;
    }
    
    function startAutoPlay() {
        const currentPage = getCurrentPageIndex();
        
        autoPlayInterval = setInterval(() => {
            const nextPageIndex = (currentPage + 1) % pages.length;
            const nextPage = pages[nextPageIndex];
            
            // Build the URL based on the controller and action
            const baseUrl = window.location.origin;
            window.location.href = `${baseUrl}/Home/${nextPage}`;
        }, 5000); // Change page every 5 seconds
    }
    
    function stopAutoPlay() {
        if (autoPlayInterval) {
            clearInterval(autoPlayInterval);
        }
    }
    
    // Start auto-play when page loads
    window.addEventListener('load', () => {
        // Only start if we're on a home page
        if (document.querySelector('.vertical-dots')) {
            startAutoPlay();
        }
    });
    
    // Stop auto-play when user clicks on dots
    document.addEventListener('click', (e) => {
        if (e.target.closest('.vertical-dots .dot')) {
            stopAutoPlay();
        }
    });
    
    // Stop auto-play when user clicks arrow
    document.addEventListener('click', (e) => {
        if (e.target.closest('.hero-arrow button')) {
            stopAutoPlay();
        }
    });
})();
