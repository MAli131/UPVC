// lang.js - Language toggle with server-side culture support
(function () {
    const RTL_CSS = '/css/style-rtl.css';
    const STORAGE_KEY = 'site-lang';

    function safeGet(id) { return document.getElementById(id); }

    function applyLangUI(lang) {
        const html = document.documentElement;
        const body = document.body;

        if (lang === 'ar') {
            html.lang = 'ar';
            html.dir = 'rtl';
            body.classList.add('rtl');

            if (!document.querySelector('link[data-rtl]')) {
                const link = document.createElement('link');
                link.rel = 'stylesheet';
                link.href = RTL_CSS;
                link.setAttribute('data-rtl', 'true');
                document.head.appendChild(link);
            }
        } else {
            html.lang = 'en';
            html.dir = 'ltr';
            body.classList.remove('rtl');

            const rtlLink = document.querySelector('link[data-rtl]');
            if (rtlLink) rtlLink.remove();
        }

        // sync radios if present
        const en = safeGet('en-lang');
        const ar = safeGet('ar-lang');
        if (lang === 'ar' && ar) ar.checked = true;
        if (lang === 'en' && en) en.checked = true;

        try { localStorage.setItem(STORAGE_KEY, lang); } catch (e) { /* ignore if storage disabled */ }
    }

    function setServerCulture(lang) {
        const culture = lang === 'ar' ? 'ar-SA' : 'en-US';
        const form = document.createElement('form');
        form.method = 'POST';
        form.action = '/Language/SetLanguage';
        
        const cultureInput = document.createElement('input');
        cultureInput.type = 'hidden';
        cultureInput.name = 'culture';
        cultureInput.value = culture;
        
        const returnUrlInput = document.createElement('input');
        returnUrlInput.type = 'hidden';
        returnUrlInput.name = 'returnUrl';
        returnUrlInput.value = window.location.pathname + window.location.search;
        
        const tokenInput = document.createElement('input');
        tokenInput.type = 'hidden';
        tokenInput.name = '__RequestVerificationToken';
        const tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
        if (tokenElement) {
            tokenInput.value = tokenElement.value;
        }
        
        form.appendChild(cultureInput);
        form.appendChild(returnUrlInput);
        form.appendChild(tokenInput);
        document.body.appendChild(form);
        form.submit();
    }

    function applyLang(lang) {
        applyLangUI(lang);
        setServerCulture(lang);
    }

    document.addEventListener('DOMContentLoaded', function () {
        const en = safeGet('en-lang');
        const ar = safeGet('ar-lang');

        // restore saved or fallback to html lang
        let saved = null;
        try { saved = localStorage.getItem(STORAGE_KEY); } catch (e) { saved = null; }
        const fallback = (document.documentElement.lang && document.documentElement.lang.toLowerCase() === 'ar') ? 'ar' : 'en';
        const initial = saved || fallback;
        applyLangUI(initial);

        if (en) en.addEventListener('change', function () { if (en.checked) applyLang('en'); });
        if (ar) ar.addEventListener('change', function () { if (ar.checked) applyLang('ar'); });
    });
})();

