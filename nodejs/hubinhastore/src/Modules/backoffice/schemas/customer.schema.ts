import * as mongoose from 'mongoose';

export const CustomerSchema = new mongoose.Schema({
    name: {
        type: String,
        required: true,
    },
    document: {
        type: String,
        required: true,
        trim: true,
        index: {
            unique: true,
        },
    },
    email: {
        type: String,
        required: true,
        trim: true,
        index: {
            unique: true,
        },
    },
    pets: [
        {
            id: {
                type: String,
            },
            name: {
                type: String,
            },
            gender: {
                type: String,
                enum: ['male', 'female', 'none'],
            },
            kind: {
                type: String,
            },
            brand: {
                type: String,
            },
        },
    ],
    billingAddress: {
        zipcode: {
            type: String,
        },
        street: {
            type: String,
        },
        number: {
            type: String,
        },
        complement: {
            type: String,
        },
        neighborhood: {
            type: String,
        },
        city: {
            type: String,
        },
        state: {
            type: String,
        },
        country: {
            type: String,
        },
    },
    shippingAddress: {
        zipcode: {
            type: String,
        },
        street: {
            type: String,
        },
        number: {
            type: String,
        },
        complement: {
            type: String,
        },
        neighborhood: {
            type: String,
        },
        city: {
            type: String,
        },
        state: {
            type: String,
        },
        country: {
            type: String,
        },
    },
    card: {
        number: {
            type: String,
        },
        holder: {
            type: String,
        },
        expiration: {
            type: String,
        },
    },
    //note that i externalize USER because i can have another kind of users that are not customer
    //for example: a vendor, a provider that can have ACCESS to the API too, but is not a customer
    user: {
        type: mongoose.Schema.Types.ObjectId, //here i say that i will save only the ID of the user
        ref: 'User',
        required: true,
    },
});
