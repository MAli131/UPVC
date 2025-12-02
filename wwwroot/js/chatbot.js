// Chatbot Widget
class ChatbotWidget {
    constructor() {
        this.apiUrl = '/api/chatbotapi';
        // Get language from HTML lang attribute or default to 'ar'
        this.language = document.documentElement.getAttribute('lang') || 'ar';
        // Get configuration from global config or use defaults
        this.whatsappNumber = window.chatbotConfig?.whatsappNumber || '';
        this.contactUrl = window.chatbotConfig?.contactUrl || '/Contact';
        this.questions = [];
        this.questionCount = 0;
        this.maxQuestions = 5;
        this.init();
    }

    async init() {
        await this.loadQuestions();
        this.render();
        this.attachEvents();
        this.restoreSession();
    }

    async loadQuestions() {
        try {
            console.log('Loading questions from:', `${this.apiUrl}/questions?lang=${this.language}`);
            const response = await fetch(`${this.apiUrl}/questions?lang=${this.language}`);
            console.log('Response status:', response.status);
            
            if (response.ok) {
                this.questions = await response.json();
                console.log('Questions loaded:', this.questions.length, this.questions);
            } else {
                console.error('Failed to load questions. Status:', response.status);
            }
        } catch (error) {
            console.error('خطأ في تحميل الأسئلة:', error);
        }
    }

    render() {
        // Format WhatsApp number (remove spaces and add country code if needed)
        const whatsappLink = this.whatsappNumber ? 
            `https://wa.me/${this.whatsappNumber.replace(/\s/g, '')}` : 
            '#';
        
        // Translations
        const translations = {
            ar: {
                openChat: 'فتح الشات بوت',
                close: 'إغلاق',
                assistantName: 'مساعد EMAPEN',
                onlineNow: 'متصل الآن',
                contactUs: 'تواصل معنا',
                whatsapp: 'واتساب',
                badge: 'اسأل الآن'
            },
            en: {
                openChat: 'Open chatbot',
                close: 'Close',
                assistantName: 'EMAPEN Assistant',
                onlineNow: 'Online now',
                contactUs: 'Contact Us',
                whatsapp: 'WhatsApp',
                badge: 'Ask Now'
            }
        };
        
        const t = translations[this.language] || translations.ar;
        
        const widgetHTML = `
            <div class="chatbot-widget" id="chatbotWidget">
                <button class="chatbot-toggle-btn" id="chatbotToggle" aria-label="${t.openChat}">
                    <i class="fas fa-comments"></i>
                    <span class="chatbot-badge" id="chatbotBadge" style="display: none;">${t.badge}</span>
                </button>
                
                <div class="chatbot-window" id="chatbotWindow">
                    <div class="chatbot-header">
                        <div class="chatbot-header-content">
                            <div class="chatbot-avatar">
                                <i class="fas fa-robot"></i>
                            </div>
                            <div class="chatbot-title">
                                <h4>${t.assistantName}</h4>
                                <p>${t.onlineNow}</p>
                            </div>
                        </div>
                        <button class="chatbot-close" id="chatbotClose" aria-label="${t.close}">
                            <i class="fas fa-times"></i>
                        </button>
                    </div>
                    
                    <div class="chatbot-messages" id="chatbotMessages"></div>
                    
                    <div class="chatbot-questions" id="chatbotQuestions"></div>
                    
                    <div class="chatbot-footer" id="chatbotFooter">
                        <div class="question-counter" id="questionCounter" style="display: none;"></div>
                        <div class="contact-options">
                            <a href="${this.contactUrl}" class="contact-btn contact-btn-primary">
                                <i class="fas fa-envelope"></i>
                                <span>${t.contactUs}</span>
                            </a>
                            ${this.whatsappNumber ? `
                            <a href="${whatsappLink}" target="_blank" class="contact-btn contact-btn-whatsapp">
                                <i class="fab fa-whatsapp"></i>
                                <span>${t.whatsapp}</span>
                            </a>
                            ` : ''}
                        </div>
                    </div>
                </div>
            </div>
        `;
        document.body.insertAdjacentHTML('beforeend', widgetHTML);
    }

    attachEvents() {
        const toggleBtn = document.getElementById('chatbotToggle');
        const closeBtn = document.getElementById('chatbotClose');
        
        toggleBtn.addEventListener('click', () => this.toggleWindow());
        closeBtn.addEventListener('click', () => this.closeWindow());
        
        // Close when clicking outside
        document.addEventListener('click', (e) => {
            const widget = document.getElementById('chatbotWidget');
            if (!widget.contains(e.target) && this.isWindowOpen()) {
                this.closeWindow();
            }
        });
    }

    toggleWindow() {
        const window = document.getElementById('chatbotWindow');
        window.classList.toggle('active');
        
        if (this.isWindowOpen() && document.getElementById('chatbotMessages').children.length === 0) {
            this.showWelcomeMessage();
            this.showQuestions();
        }
        
        // Hide badge when opened
        const badge = document.getElementById('chatbotBadge');
        badge.style.display = 'none';
    }

    closeWindow() {
        document.getElementById('chatbotWindow').classList.remove('active');
    }

    isWindowOpen() {
        return document.getElementById('chatbotWindow').classList.contains('active');
    }

    showWelcomeMessage() {
        const welcomeText = this.language === 'ar' 
            ? 'مرحباً! أنا مساعد EMAPEN الذكي. يمكنني الإجابة على أسئلتك حول أنظمة uPVC ومنتجاتنا. اختر سؤالاً من القائمة أدناه:'
            : 'Welcome! I am EMAPEN smart assistant. I can answer your questions about uPVC systems and our products. Choose a question from the list below:';
        
        this.addBotMessage(welcomeText);
    }

    showQuestions() {
        const questionsContainer = document.getElementById('chatbotQuestions');
        questionsContainer.innerHTML = '';
        
        console.log('showQuestions called. Question count:', this.questionCount, 'Total questions:', this.questions.length);
        
        if (this.questionCount >= this.maxQuestions) {
            this.showContactPrompt();
            return;
        }
        
        if (!this.questions || this.questions.length === 0) {
            console.error('No questions available!');
            questionsContainer.innerHTML = '<p style="text-align: center; padding: 20px; color: #999;">لا توجد أسئلة متاحة حالياً</p>';
            return;
        }
        
        this.questions.forEach(q => {
            const btn = document.createElement('button');
            btn.className = 'question-btn';
            btn.textContent = q.question;
            // Pass only ID and question text, fetch answer on click
            btn.addEventListener('click', () => this.handleQuestionClick(q.id, q.question));
            questionsContainer.appendChild(btn);
        });
        
        this.updateQuestionCounter();
    }

    async handleQuestionClick(questionId, questionText) {
        // Show user's question
        this.addUserMessage(questionText);
        
        // Hide questions temporarily
        document.getElementById('chatbotQuestions').style.display = 'none';
        
        // Show typing indicator
        this.showTypingIndicator();
        
        try {
            // Fetch the full answer from API
            const response = await fetch(`${this.apiUrl}/question/${questionId}?lang=${this.language}`);
            
            if (!response.ok) {
                throw new Error('Failed to fetch answer');
            }
            
            const faq = await response.json();
            
            // Remove typing indicator
            this.removeTypingIndicator();
            
            // Show answer
            this.addBotMessage(faq.answer);
            
        } catch (error) {
            console.error('Error fetching answer:', error);
            this.removeTypingIndicator();
            this.addBotMessage('عذراً، حدث خطأ في تحميل الإجابة. يرجى المحاولة مرة أخرى.');
        }
        
        // Increment question count
        this.questionCount++;
        this.saveSession();
        
        // Show questions again or contact prompt
        document.getElementById('chatbotQuestions').style.display = 'block';
        
        if (this.questionCount >= this.maxQuestions) {
            this.showContactPrompt();
        } else {
            this.showQuestions();
        }
    }

    addBotMessage(text) {
        const messagesContainer = document.getElementById('chatbotMessages');
        
        // Check if text is empty
        if (!text || text.trim() === '') {
            console.error('Bot message is empty!');
            text = 'عذراً، لا توجد إجابة متاحة حالياً.';
        }
        
        // Replace newlines with <br> for proper display
        const formattedText = text.replace(/\n/g, '<br>');
        
        const messageHTML = `
            <div class="chatbot-message message-bot">
                <div class="message-avatar">
                    <i class="fas fa-robot"></i>
                </div>
                <div class="message-content">
                    <p>${formattedText}</p>
                </div>
            </div>
        `;
        messagesContainer.insertAdjacentHTML('beforeend', messageHTML);
        
        // Add class to show messages area
        messagesContainer.classList.add('has-messages');
        
        this.scrollToBottom();
    }

    addUserMessage(text) {
        const messagesContainer = document.getElementById('chatbotMessages');
        
        // Check if text is empty
        if (!text || text.trim() === '') {
            console.error('User message is empty!');
            return;
        }
        
        const messageHTML = `
            <div class="chatbot-message message-user">
                <div class="message-content">
                    <p>${text}</p>
                </div>
            </div>
        `;
        messagesContainer.insertAdjacentHTML('beforeend', messageHTML);
        
        // Add class to show messages area
        messagesContainer.classList.add('has-messages');
        
        this.scrollToBottom();
    }

    showTypingIndicator() {
        const messagesContainer = document.getElementById('chatbotMessages');
        const typingHTML = `
            <div class="chatbot-message message-bot typing-indicator-wrapper">
                <div class="message-avatar">
                    <i class="fas fa-robot"></i>
                </div>
                <div class="message-content">
                    <div class="typing-indicator">
                        <div class="typing-dot"></div>
                        <div class="typing-dot"></div>
                        <div class="typing-dot"></div>
                    </div>
                </div>
            </div>
        `;
        messagesContainer.insertAdjacentHTML('beforeend', typingHTML);
        this.scrollToBottom();
    }

    removeTypingIndicator() {
        const indicator = document.querySelector('.typing-indicator-wrapper');
        if (indicator) {
            indicator.remove();
        }
    }

    showContactPrompt() {
        document.getElementById('chatbotQuestions').innerHTML = '';
        
        const promptText = this.language === 'ar'
            ? 'لقد وصلت إلى الحد الأقصى من الأسئلة. للمزيد من المساعدة، تواصل معنا مباشرة:'
            : 'You have reached the maximum number of questions. For more help, contact us directly:';
        
        this.addBotMessage(promptText);
        
        // Counter already visible, just update it
        const counter = document.getElementById('questionCounter');
        counter.textContent = this.language === 'ar' 
            ? 'تم استنفاد الأسئلة المتاحة'
            : 'Question limit reached';
        counter.style.display = 'block';
    }

    updateQuestionCounter() {
        const counter = document.getElementById('questionCounter');
        
        if (this.questionCount > 0) {
            const remaining = this.maxQuestions - this.questionCount;
            const text = this.language === 'ar'
                ? `يمكنك طرح ${remaining} ${remaining === 1 ? 'سؤال' : 'أسئلة'} إضافية`
                : `You can ask ${remaining} more question${remaining !== 1 ? 's' : ''}`;
            
            counter.textContent = text;
            counter.style.display = 'block';
        } else {
            counter.style.display = 'none';
        }
    }

    scrollToBottom() {
        const messagesContainer = document.getElementById('chatbotMessages');
        messagesContainer.scrollTop = messagesContainer.scrollHeight;
    }

    saveSession() {
        sessionStorage.setItem('chatbotQuestionCount', this.questionCount.toString());
    }

    restoreSession() {
        const savedCount = sessionStorage.getItem('chatbotQuestionCount');
        if (savedCount) {
            this.questionCount = parseInt(savedCount, 10);
        }
    }

    // Show badge when page loads (optional - to attract attention)
    showBadge() {
        const badge = document.getElementById('chatbotBadge');
        if (badge) {
            badge.style.display = 'block';
            // Auto-hide after 5 seconds
            setTimeout(() => {
                badge.style.display = 'none';
            }, 5000);
        }
    }
}

// Initialize chatbot when DOM is ready
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => {
        const chatbot = new ChatbotWidget();
        // Optional: Show badge to attract attention on first load
        setTimeout(() => chatbot.showBadge(), 2000);
    });
} else {
    const chatbot = new ChatbotWidget();
    setTimeout(() => chatbot.showBadge(), 2000);
}
