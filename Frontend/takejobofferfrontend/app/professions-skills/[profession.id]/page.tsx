"use client";

import Title from "antd/es/typography/Title";
import { useEffect, useState } from "react";
import { getProfessionSkillsWithNames } from "../../services/professionSkills";
import { ProfessionSkills } from "../../components/ProfessionSkills";

interface Props {
    profession: Profession;
};

export default function ProfessionSkillsPage({profession}: Props) {
  const [professionSkills, setProfessionSkills] = useState<ProfessionSkillWithName[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getSkillsByProfessionId = async (professionId: string) => {
        const professionSkills = await getProfessionSkillsWithNames(professionId);

        setLoading(false);
        setProfessionSkills(professionSkills);
    }
    
    getSkillsByProfessionId(profession.id)
  });

  return (
    <div>
      {loading ? (
        <Title>Loading...</Title>
      ) : (
        <ProfessionSkills 
            professionSkills={professionSkills}
        />
      )}
    </div>
  );
};
