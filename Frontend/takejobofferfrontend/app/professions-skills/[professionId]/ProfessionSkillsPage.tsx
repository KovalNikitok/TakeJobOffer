"use client"

import Title from "antd/es/typography/Title";
import { useEffect, useState } from "react";
import { getProfessionSkillsWithNamesISR } from "../../services/professionSkills";
import { ProfessionSkills } from "../../components/ProfessionSkills";

interface ProfessionSkillsProps {
    professionId:  string;
  }

export default function ProfessionSkillsClient({ professionId }: ProfessionSkillsProps) {
    const [professionSkills, setProfessionSkills] = useState<ProfessionSkillWithName[]>([]);
    const [loading, setLoading] = useState(true);
  
    useEffect(() => {
      const getSkillsByProfessionId = async () => {
          const professionSkills = await getProfessionSkillsWithNamesISR(professionId, 600);
  
          setLoading(false);
          setProfessionSkills(professionSkills);
      }
      
      getSkillsByProfessionId();
    }, []);
  
    return (
      <div>
        {
          loading ? (
            <Title>Loading...</Title>
          ) : (
            <ProfessionSkills professionSkills={professionSkills} />
          )
        }
      </div>
    );
}