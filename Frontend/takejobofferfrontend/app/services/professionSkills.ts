import { host } from "./host";

interface ProfessionSkillRequest {
    skillId: string;
    skillMentionCount: number;
}

export const getProfessionSkills = async (professionId: string) => {
    return await fetch(
        `${host}/api/professions-skills/${professionId}`, {
            method: "GET",
            headers: {
                "accept": "application/json"
            },
        }).then(response => {
            if(!response.ok) {
                console.log(new Error(response.statusText));
                return ([]); 
            }
            return response.json() as Promise<ProfessionSkill[]>;
        });
};

export const getProfessionSkillsWithNames = async (professionId: string, isOrdered: boolean = false) => {
    return await fetch(
        `${host}/api/professions-skills/${professionId}/with-name?isOrdered=${isOrdered}`, {
            method: "GET",
            headers: {
                "accept": "application/json"
            },
        }).then(response => {
            if(!response.ok) {
                console.log(new Error(response.statusText));
                return ([]); 
            }
            return response.json() as Promise<ProfessionSkillWithName[]>;
        });
};

export const getProfessionSkillsWithNamesISR = async (professionId: string, isrDuration: number) => {
    return await fetch(
        `${host}/api/professions-skills/${professionId}/with-name`, {
            method: "GET",
            headers: {
                "accept": "application/json"
            },
            next: { revalidate: isrDuration },
        }).then(response => {
            if(!response.ok) {
                console.log(new Error(response.statusText));
                return ([]); 
            }
            return response.json() as Promise<ProfessionSkillWithName[]>;
        });
};

export const getProfessionSkill = async (professionId: string, skillId: string) => {
    return await fetch(
        `${host}/api/professions-skills/${professionId}`, {
            method: "GET",
            headers: {
                "accept": "application/json"
            },
            body: JSON.stringify(skillId),
        }).then(response => {
            if(!response.ok) {
                console.log(new Error(response.statusText));
                return ({ }); 
            }
            return response.json() as Promise<ProfessionSkill>
        });
};