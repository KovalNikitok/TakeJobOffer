import { baseApiUrl } from "./baseApiUrl";

export interface SkillRequest {
  name: string;
}

export const getAllSkills = async () => {
  return await fetch(`${baseApiUrl}/skills`, {
    method: "GET",
    headers: {
      "accept": "application/json",
    },
  })
    .then((response) => {
        if (!response.ok) {
            throw new Error(response.statusText);
        }
        return response.json() as Promise<Skill[]>;
    });
};

export const getSkillById = async (id:string) => {
  return await fetch(`${baseApiUrl}/skills/${id}`, {
    method: "GET",
    headers: {
        "accept": "application/json",
    },
  })
    .then(response => {
        if(!response.ok) {
            throw new Error(response.statusText);
        }
        return response.json() as Promise<Skill>;
    });
};

export const createSkill = async (skillRequest: SkillRequest) => {
    await fetch(`${baseApiUrl}/api/skills`, {
        method: "POST",
        headers: {
            "content-type": "application/json",
        },
        body: JSON.stringify(skillRequest),
    })
        .then((response) => {
            if(!response.ok)
                throw new Error(response.statusText);
        });
};

export const updateSkill = async (id: string, skillRequest: SkillRequest) => {
    await fetch(`${baseApiUrl}/skills/${id}`, {
        method: "PUT",
        headers: {
            "content-type": "application/json"
        },
        body: JSON.stringify(skillRequest),
    })
        .then(response => {
            if(!response.ok) {
                throw new Error(response.statusText);
            }
        });
};

export const deleteSkill = async (id: string) => {
    await fetch(`${baseApiUrl}/skills/${id}`, {
        method: "DELETE",
    })
        .then(response => {
            if(!response.ok)
                throw new Error(response.statusText);
        });
};