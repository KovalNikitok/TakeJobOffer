interface ProfessionSkillRequest {
    skillId: string;
    skillMentionCount: number;
}

export const getProfessionSkills = async (professionId: string) => {
    return await fetch(
        `https://localhost:8081/api/professions-skills/${professionId}`, {
            method: "GET",
            headers: {
                "accept": "application/json"
            },
        }).then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<ProfessionSkill[]>
        });
};

export const getProfessionSkillsWithNames = async (professionId: string) => {
    return await fetch(
        `https://localhost:8081/api/professions-skills/${professionId}/with-name`, {
            method: "GET",
            headers: {
                "accept": "application/json"
            },
        }).then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<ProfessionSkillWithName[]>
        });
};

export const getProfessionSkill = async (professionId: string, skillId: string) => {
    return await fetch(
        `https://localhost:8081/api/professions-skills/${professionId}`, {
            method: "GET",
            headers: {
                "accept": "application/json"
            },
            body: JSON.stringify(skillId),
        }).then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
            return response.json() as Promise<ProfessionSkill[]>
        });
};