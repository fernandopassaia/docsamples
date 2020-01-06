//eu não preciso criar uma classe, apenas uma representação do dado. Então uma interface nesse caso é muito útil.

export interface Restaurant{

    id: string
    name: string
    category: string
    deliveryEstimate: string
    rating: number
    imagePath: string
}