(function () {
    function humanizeDate(ms) {
        var seconds = Math.floor((Date.now() - ms) / 1000);
        var intervals = [
            [31536000, 'year'],
            [2592000,  'month'],
            [604800,   'week'],
            [86400,    'day'],
            [3600,     'hour'],
            [60,       'minute']
        ];
        for (var i = 0; i < intervals.length; i++) {
            var count = Math.floor(seconds / intervals[i][0]);
            if (count >= 1) {
                return count + ' ' + intervals[i][1] + (count > 1 ? 's' : '') + ' ago';
            }
        }
        return 'just now';
    }

    document.addEventListener('DOMContentLoaded', function () {
        var elements = document.querySelectorAll('time.humanize-date[data-timestamp]');
        elements.forEach(function (el) {
            var ts = parseInt(el.getAttribute('data-timestamp'), 10);
            if (!isNaN(ts)) {
                el.textContent = humanizeDate(ts);
            }
        });
    });
})();
