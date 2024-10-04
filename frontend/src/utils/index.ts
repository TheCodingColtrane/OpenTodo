import { User } from "../types";

export const convertDateLocaleBRToDateOnly = (date: string) => {
    const dateParts = date.split('/')
    return dateParts[2] + "-" + dateParts[1] + "-" + dateParts[0]
}

export const maskDate = (
    evt: React.KeyboardEvent<HTMLInputElement>,
    text: HTMLInputElement
) => {
    let date = text.value;
    if (!date) return '';
    const key = evt.key;
    let char = '';
    if (date.length === 2 || date.length === 5) {
        char = addSpecialCharacters('/', key);
        if (char) date += char;
        text.value = date;
    }
    text.value = date;
};

const addSpecialCharacters = (char: string, key: string) => {
    if (key === 'Backspace' || key === 'Delete') {
        return '';
    } else {
        return char;
    }
};

export const isDate = (text: string) => !isNaN(Date.parse(text))

export const convertDateOnlyToDateLocaleBR = (text: string) => {
    const dateParts = text.split('-');
    return dateParts[2] + "/" + dateParts[1] + "/" + dateParts[0]
}

export const showMessage = (message: string) => {
    const toaster = document.getElementById("snackbar") as HTMLElement
    toaster.className = "show";
    toaster.innerHTML = message;
    setTimeout(function () { toaster.className = toaster.className.replace("show", ""); }, 3000)
}

export const getAllCookies = () => {
    const rawCookies = document.cookie
    const splittedCookies = rawCookies.split(';')
    const cookies = [{ name: "", value: "" }]
    if (splittedCookies.length === 1) return cookies

    for (let i = 0; i < splittedCookies.length; i++) {
        if (i === 0) {
            const cookie = splittedCookies[0].split("=")
            cookies[0].name = cookie[0].trimStart()
            cookies[0].value = cookie[1]
            continue;
        }
        const cookie = splittedCookies[i].split("=")
        cookies.push({ name: cookie[0].trimStart(), value: cookie[1] })
        continue;
    }
    return cookies

}



export const findCookie = (name: string) => {
    return getAllCookies().find(c => c.name === name)
}

export const createCookie = (name: string, value: string, exp: number) => {
    document.cookie = `${name}=${value};Path=/; Max-Age=${exp}`
}

export const deleteCookie = (name: string, value: string) => {
    document.cookie = `${name}=${value};Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT}`

}


export const userCredentials = {
    name: findCookie("name")?.value,
    username: findCookie("username")?.value,
    sid: findCookie("sid")?.value,
}

export const autocomplete = async (inp: HTMLInputElement, arr: User[]) => {
    let currentFocus = 0;
    inp.addEventListener("input", function () {
        let a: HTMLElement = document.body
        let b, i
        const val = this.value;
        closeAllLists(null);
        if (!val) { return false; }
        currentFocus = -1;
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        this.parentNode?.appendChild(a);
        for (i = 0; i < arr.length; i++) {
                // arr[i].firstName += " " + arr[i].lastName
            if (arr[i].firstName.substring(0, val.length).toUpperCase() == val.toUpperCase() || 
            arr[i].lastName.substring(0, val.length).toUpperCase() == val.toUpperCase() || 
            arr[i].username.substring(0, val.length).toUpperCase() == val.toUpperCase()) {
                b = document.createElement("DIV");
                b.innerHTML = "<strong>" + arr[i].firstName.substring(0, val.length) + " " + arr[i].lastName + "</strong>";
                b.innerHTML += arr[i].firstName.substring(val.length);
                b.innerHTML += "<input type='hidden' value='" + arr[i].firstName + " " + arr[i].lastName + "' data-uc='" + arr[i].code + "'>";
                b.addEventListener("click", function () {
                    inp.value = this.getElementsByTagName("input")[0].value;
                    for(const user of arr){
                        if((user.firstName + " " + user.lastName) === inp.value){
                            inp.dataset.uc = user.code
                        }
                    }
                    closeAllLists(null);
                });
                a.appendChild(b);
            }
        }
    });
    inp.addEventListener("keydown", function (e) {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        let x = document.getElementById(this.id + "autocomplete-list") as any;
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            currentFocus++;
            addActive(x);
        } else if (e.keyCode == 38) {
            currentFocus--;
            addActive(x);
        } else if (e.keyCode == 13) {
            e.preventDefault();
            if (currentFocus > -1) {
                if (x) x[currentFocus].click();
            }
        }
        inp.dataset.uc = ""
    });
    const addActive = (x: HTMLCollectionOf<HTMLDivElement>) => {
        if (!x) return false;
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        x[currentFocus].classList.add("autocomplete-active");
        inp.dataset.uc = ""
    }

    const removeActive = (x: HTMLCollectionOf<HTMLDivElement>) => {
        for (let i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const closeAllLists = (elmnt: any | undefined) => {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const x = document.getElementsByClassName("autocomplete-items") as HTMLCollectionOf<any>;
        for (let i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
} 