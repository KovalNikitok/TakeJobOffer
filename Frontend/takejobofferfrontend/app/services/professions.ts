export interface ProfessionRequest {
    name: string;
    description: string;
}

export const getAllProfessions = async () => {
    return fetch("http://localhost:5113/professions", {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<Profession[]>
        });
};

export const createProfession = async (professionRequest: ProfessionRequest) => {
    await fetch("http://localhost:5113/professions", {
        method: "POST",
        headers: {
            "content-type": "application/json",
        },
        body: JSON.stringify(professionRequest),
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
        });
};

export const updateProfession = async (id: string, professionRequest: ProfessionRequest) => {
    await fetch(`http://localhost:5113/professions/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(professionRequest),
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
        });
};

export const deleteProfession = async (id: string) => {
    await fetch(`http://localhost:5113/professions/${id}`, {
            method: "DELETE",
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
        });
};