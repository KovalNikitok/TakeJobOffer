import { host } from "./host";

export interface ProfessionSlugRequest {
    slug: string;
}

export const getAllProfessionsSlug = async () => {
    const response = await fetch(`${host}/api/professions-slug`, {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
    });
    if(!response.ok) {
        console.log(response.statusText);
        return ([]);    
    }

    const data: ProfessionSlug[] = await response.json();
        
    return data;
};

export const getProfessionSlugById = async (id: string) => {
    return await fetch(`${host}/api/professions/${id}`, {
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

export const getAllProfessionsSlugISR = async (isrDuration: number) => {
    const response = await fetch(`${host}/api/professions-slug`, {
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

    const data: ProfessionSlug[] = await response.json();
    
    return data;
};

export const getProfessionSlugByProfessionId = async (professionId: string) => {
    return await fetch(`${host}/api/professions-slug/p/${professionId}`, {
        method: "GET",
        headers: {
            "accept": "application/json"
        },
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<ProfessionSlug>
        });
};