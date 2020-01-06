//criei essa classe mais no FINAL do curso, e usarei ela basicamente pra representar
//minha compra e enviar pro backEnd. Então terei duas classes aqui: Order e OrderItem

class Order{
    constructor(
        public address:string,
        public number: number,
        public optionalAddress: string,
        public paymentOpetion: string,
        public orderItems: OrderItem[] = []
    ){}
}

//irei enviar só o código (ID) e a quantidade, presume-se que o backend já tenha preço e descrição
class OrderItem{
    constructor(public quantity: number, public menuId: string){}
}

//pronto, criei um módulo com minhas duas classes e exportei ele
export {Order, OrderItem}