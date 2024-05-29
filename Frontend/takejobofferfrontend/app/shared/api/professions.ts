import { baseApiUrl } from "./baseApiUrl";

export interface ProfessionRequest {
    name: string;
    description: string;
}

export interface ProfessionWithSlugRequest {
    name: string;
    description: string;
    slug: string;
}



export const getAllProfessions = async () => {
    return await fetch(`${baseApiUrl}/professions`, {
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

export const getAllProfessionsWithSlug = async () => {
    return await fetch(`${baseApiUrl}/professions/with-slug`, {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<ProfessionWithSlug[]>
        });
};

export const getProfessionById = async (id: string) => {
    return await fetch(`${baseApiUrl}/professions/${id}`, {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<Profession>
        });
};

export const getProfessionByIdWithSlug = async (id: string) => {
    return await fetch(`${baseApiUrl}/professions/${id}/with-slug`, {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<ProfessionWithSlug>
        });
};

export const getAllProfessionsISR = async (isrDuration: number) => {
    const response = await fetch(`${baseApiUrl}/professions`, {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
        next: { revalidate: isrDuration }
    });
    if(!response.ok) {
        console.log(response.statusText);
        return ([]);    
    }

    const data: Profession[] = await response.json();
    
    return data;
};

export const getAllProfessionsWithSlugISR = async (isrDuration: number) => {
    const response = await fetch(`${baseApiUrl}/professions/with-slug`, {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
        next: { revalidate: isrDuration }
    });
    if(!response.ok) {
        console.log(response.statusText);
        return ([]);    
    }

    const data: ProfessionWithSlug[] = await response.json();
    
    return data;
};

export const getProfessionBySlug = async (slug: string) => {
    return await fetch(`${baseApiUrl}/professions/${slug}`, {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<Profession>
        });
};
export const createProfession = async (professionRequest: ProfessionRequest) => {
    await fetch(`${baseApiUrl}/professions`, {
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

export const createProfessionWithSlug = async (professionRequest: ProfessionWithSlugRequest) => {
    await fetch(`${baseApiUrl}/professions/with-slug`, {
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
    await fetch(`${baseApiUrl}/professions/${id}`, {
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

export const updateProfessionWithSlug = async (id: string, professionRequest: ProfessionWithSlugRequest) => {
    await fetch(`${baseApiUrl}/professions/${id}/with-slug`, {
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
    await fetch(`${baseApiUrl}/professions/${id}`, {
            method: "DELETE",
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
        });
};