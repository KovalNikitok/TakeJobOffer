import { host } from "./host";

export interface SkillRequest {
  name: string;
}

export const getAllSkills = async () => {
  return await fetch(`${host}/api/skills`, {
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
  return await fetch(`${host}/api/skills/${id}`, {
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
    await fetch(`${host}/api/skills`, {
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
    await fetch(`${host}/api/skills/${id}`, {
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
    await fetch(`${host}/api/skills/${id}`, {
        method: "DELETE",
    })
        .then(response => {
            if(!response.ok)
                throw new Error(response.statusText);
        });
};