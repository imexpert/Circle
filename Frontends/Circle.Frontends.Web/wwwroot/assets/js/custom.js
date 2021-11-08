Inputmask({
    "mask": "99/99/9999",
    "clearIncomplete": true,
}).mask(".Tarih");

Inputmask({
    "mask": "+99 (999) 999 99 99",
    "clearIncomplete": true
}).mask(".Telefon");

// Email address
Inputmask({
    mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,2}]",
    "clearIncomplete": true,
    greedy: false,
    onBeforePaste: function (pastedValue, opts) {
        pastedValue = pastedValue.toLowerCase();
        return pastedValue.replace("mailto:", "");
    },
    definitions: {
        "*": {
            validator: '[0-9A-Za-z!#$%&"*+/=?^_`{|}~\-]',
            cardinality: 1,
            casing: "lower"
        }
    }
}).mask(".Email");