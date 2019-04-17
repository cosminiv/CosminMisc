class Car{
    Name: string;

    constructor(name: string) {
        this.Name = name;
    }

    start() {
        console.log("starting " + this.Name);
    }

    stop() {
        console.log("stopping " + this.Name);
    }
}

var car: Car = new Car("Dacia");
car.start();
car.stop();