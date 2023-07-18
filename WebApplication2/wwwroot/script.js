let cars = [];
let brands = [];
let rentals = [];
let noncruds = [];
let connection = null;
getcardata();
getbranddata();
getrentaldata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:42910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarCreated", (user, message) => {
        getcardata();
    });

    connection.on("CarDeleted", (user, message) => {
        getcardata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

/// car

async function getcardata() {
    await fetch('http://localhost:42910/Car')
        .then(x => x.json())
        .then(y => {
            cars = y;
            console.log(cars);
            displaycar();
        });
}

function displaycar() {
    document.getElementById('carresult').innerHTML = "";
    cars.forEach(t => {
        document.getElementById('carresult').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.brandId + "</td><td>" +
            t.model + "</td><td>" +
            t.age + "</td><td>" +
            t.price + "</td><td>" +
            `<button type="button" onclick="removecar(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}

function removecar(id) {
    fetch('http://localhost:42910/car/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getcardata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function createcar() {
    let brand = document.getElementById('carbrandid').value;
    let model = document.getElementById('carmodel').value;
    let age = document.getElementById('carage').value;
    let price = document.getElementById('carprice').value;
    let car = {
        brandId: brand,
        model: model,
        age: age,
        price: price
    };
    fetch('http://localhost:42910/car', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(car)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getcardata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function updatecar() {
    let id = document.getElementById('carid').value;
    let brand = document.getElementById('carbrandid').value;
    let model = document.getElementById('carmodel').value;
    let age = document.getElementById('carage').value;
    let price = document.getElementById('carprice').value;
    let car = {
        id: id,
        brandId: brand,
        model: model,
        age: age,
        price: price
    };
    //console.log("fetch")
    fetch('http://localhost:42910/car', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(car)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getcardata();
        })
        .catch((error) => { console.error('Error:', error); });

}

///brand

async function getbranddata() {
    await fetch('http://localhost:42910/brand')
        .then(x => x.json())
        .then(y => {
            brands = y;
            console.log(brands);
            displaybrand();
        });
}

function displaybrand() {
    document.getElementById('brandresult').innerHTML = "";
    brands.forEach(t => {
        document.getElementById('brandresult').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
            `<button type="button" onclick="removebrand(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}

function removebrand(id) {
    fetch('http://localhost:42910/brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function createbrand() {
    let brandname = document.getElementById('brandname').value;
    let brand = {
        name: brandname
    };
    //console.log("fetch")
    fetch('http://localhost:42910/brand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(brand)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function updatebrand() {
    let id = document.getElementById('brandid').value;
    let brandname = document.getElementById('brandname').value;
    let brand = {
        id: id,
        name: brandname
    };
    fetch('http://localhost:42910/brand', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(brand)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });

}

/// rental

async function getrentaldata() {
    await fetch('http://localhost:42910/rental')
        .then(x => x.json())
        .then(y => {
            rentals = y;
            console.log(rentals);
            displayrental();
        });
}

function displayrental() {
    document.getElementById('rentalresult').innerHTML = "";
    rentals.forEach(t => {
        document.getElementById('rentalresult').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.carId + "</td><td>"
            + t.date + "</td><td>" +
            `<button type="button" onclick="removerental(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}

function removerental(id) {
    fetch('http://localhost:42910/rental/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getrentaldata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function createrental() {
    let carid = document.getElementById('rentalcarid').value;
    let date = document.getElementById('rentaldate').value;
    let rental = {
        carId: carid,
        date: date
    };
    fetch('http://localhost:42910/rental', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(rental)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getrentaldata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function updaterental() {
    let id = document.getElementById('rentalid').value;
    let carid = document.getElementById('rentalcarid').value;
    let date = document.getElementById('rentaldate').value;
    let rental = {
        id: id,
        carId: carid,
        date: date
    };
    fetch('http://localhost:42910/rental', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(rental)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getrentaldata();
        })
        .catch((error) => { console.error('Error:', error); });

}

/// non-crud

function displayrentcountbybrand() {
    document.getElementById('noncrudresult').innerHTML =
        `<thead>
            <tr>
                <th>Name</th>
                <th>Count</th>
            </tr>
        </thead>
        <tbody>`;

    noncruds.forEach(t => {
        document.getElementById('noncrudresult').innerHTML +=
            "<tr><td>" + t.name + "</td><td>"
            + t.count + "</td></tr>";
    });

    document.getElementById('noncrudresult').innerHTML += '</tbody>';
}

function rentcountbybrand() {
    console.log("starting noncrud");
    fetch('http://localhost:42910/rental/RentCountByBrand')
        .then(x => x.json())
        .then(y => {
            noncruds = y;
            console.log(noncruds);
            displayrentcountbybrand();
        });
}

function displayrentedaftermarch() {
    document.getElementById('noncrudresult').innerHTML =
        `<thead>
            <tr>
                <th>ID</th>
                <th>Model</th>
            </tr>
        </thead>
        <tbody>`;

    noncruds.forEach(t => {
        document.getElementById('noncrudresult').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.model + "</td></tr>";
    });

    document.getElementById('noncrudresult').innerHTML += '</tbody>';
}

function rentedaftermarch() {
    console.log("starting noncrud");
    fetch('http://localhost:42910/rental/RentedAfterMarch')
        .then(x => x.json())
        .then(y => {
            noncruds = y;
            console.log(noncruds);
            displayrentedaftermarch();
        });
}

function displaymostpopular(t) {
    document.getElementById('noncrudresult').innerHTML =
        `<thead>
            <tr>
                <th>ID</th>
                <th>Model</th>
                <th>Age</th>
                <th>Price</th>
            </tr>
        </thead>
        <tbody>`;

    document.getElementById('noncrudresult').innerHTML +=
        "<tr><td>" + t.id + "</td><td>"
        + t.model + "</td><td>"
        + t.age + "</td><td>"
        + t.price + "</td></tr>";

    document.getElementById('noncrudresult').innerHTML += '</tbody>';
}

function mostpopular() {
    console.log("starting noncrud");
    fetch('http://localhost:42910/rental/MostPopular')
        .then(x => x.json())
        .then(y => {
            console.log(y);
            displaymostpopular(y);
        });
}

function displaycountbybrand() {
    document.getElementById('noncrudresult').innerHTML =
        `<thead>
            <tr>
                <th>Brand</th>
                <th>Count</th>
            </tr>
        </thead>
        <tbody>`;

    noncruds.forEach(t => {
        document.getElementById('noncrudresult').innerHTML +=
            "<tr><td>" + t.name + "</td><td>"
            + t.count + "</td></tr>";
    });

    document.getElementById('noncrudresult').innerHTML += '</tbody>';
}

function countbybrand() {
    console.log("starting noncrud");
    fetch('http://localhost:42910/car/CountByBrand')
        .then(x => x.json())
        .then(y => {
            noncruds = y;
            console.log(noncruds);
            displaycountbybrand();
        });
}

function displayavgbybrand() {
    document.getElementById('noncrudresult').innerHTML =
        `<thead>
            <tr>
                <th>Brand</th>
                <th>AVG</th>
            </tr>
        </thead>
        <tbody>`;

    noncruds.forEach(t => {
        document.getElementById('noncrudresult').innerHTML +=
            "<tr><td>" + t.name + "</td><td>"
            + t.avg + "</td></tr>";
    });

    document.getElementById('noncrudresult').innerHTML += '</tbody>';
}

function avgbybrand() {
    console.log("starting noncrud");
    fetch('http://localhost:42910/brand/AVGByBrand')
        .then(x => x.json())
        .then(y => {
            noncruds = y;
            console.log(noncruds);
            displayavgbybrand();
        });
}