import Joi from 'joi';

const carSchema = Joi.object({
    brand: Joi.string()
    .trim()
    .min(1)
    .max(50)
    .required()
    .messages({
        'string.empty': 'Märke är obligatoriskt',
        'string.min': 'Märke måste ha minst 1 tecken',
        'string.max': 'Märke får ha högst 50 tecken',
        'any.required': 'Märke är obligatoriskt',
    }),
    regNumber: Joi.string()
    .trim()
    .custom((value, helpers) => {
        // Normalize: remove spaces, convert to uppercase
        const normalized = value.replace(/\s/g, '').toUpperCase();
        // Validate AAA123 or AAA77A
        if (/^[A-Z]{3}\d{3}$/.test(normalized) || /^[A-Z]{3}\d{2}[A-Z]$/.test(normalized)) {
            return normalized; // Return normalized value (e.g., 'ABC77A')
        }
        return helpers.message('Registreringsnummer måste vara i formatet AAA123 eller AAA77A');
    })
    .required()
    .messages({
        'string.empty': 'Registreringsnummer är obligatoriskt',
        'any.required': 'Registreringsnummer är obligatoriskt',
    }),
});

export { carSchema };