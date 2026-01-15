// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Utility Functions
const Utils = {
    /**
     * Scroll to bottom of page smoothly
     * @param {number} offset - Optional offset from bottom in pixels (default: 0)
     */
    scrollToBottom: function(offset = 0) {
        const documentHeight = document.documentElement.scrollHeight;
        const windowHeight = window.innerHeight;
        const scrollPosition = documentHeight - windowHeight - offset;
        
        window.scrollTo({
            top: scrollPosition,
            behavior: 'smooth'
        });
    },

    /**
     * Scroll to a specific element
     * @param {string|HTMLElement} target - CSS selector or element
     * @param {number} offset - Optional offset from element in pixels (default: 0)
     */
    scrollToElement: function(target, offset = 0) {
        const element = typeof target === 'string' ? document.querySelector(target) : target;
        
        if (element) {
            const elementPosition = element.getBoundingClientRect().top + window.pageYOffset;
            const offsetPosition = elementPosition - offset;
            
            window.scrollTo({
                top: offsetPosition,
                behavior: 'smooth'
            });
        }
    },

    /**
     * Auto-scroll to bottom when element is visible/focused
     * @param {string|HTMLElement} triggerElement - Element that triggers scroll
     * @param {number} delay - Delay in milliseconds before scrolling (default: 100)
     */
    autoScrollOnFocus: function(triggerElement, delay = 100) {
        const element = typeof triggerElement === 'string' ? document.querySelector(triggerElement) : triggerElement;
        
        if (element) {
            element.addEventListener('focus', function() {
                setTimeout(() => Utils.scrollToBottom(), delay);
            });
        }
    },

    /**
     * Check if element is in viewport
     * @param {string|HTMLElement} element - CSS selector or element
     * @returns {boolean}
     */
    isInViewport: function(element) {
        const el = typeof element === 'string' ? document.querySelector(element) : element;
        if (!el) return false;
        
        const rect = el.getBoundingClientRect();
        return (
            rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );
    },

    /**
     * Scroll to bottom if element is not in viewport
     * @param {string|HTMLElement} element - CSS selector or element to check
     */
    ensureVisible: function(element) {
        if (!Utils.isInViewport(element)) {
            Utils.scrollToElement(element, 20);
        }
    }
};

// Make Utils globally available
window.Utils = Utils;
