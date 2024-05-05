"use client"

import Title from "antd/es/typography/Title";
import { useEffect, useState } from "react";
import { getProfessionSkillsWithNamesISR } from "../../services/professionSkills";
import { ProfessionSkills } from "../../components/ProfessionSkills";
import { getProfessionBySlug } from "@/app/services/professions";

interface ProfessionSkillsProps {
    professionSlug:  string;
  }

export default function ProfessionSkillsClient({ professionSlug }: ProfessionSkillsProps) {
    const [professionSkills, setProfessionSkills] = useState<ProfessionSkillWithName[]>([]);
    const [profession, setProfession] = useState<Profession>();
    const [loading, setLoading] = useState(true);
  
    useEffect(() => {
      const getSkillsByProfessionSlug = async () => {
          const profession = await getProfessionBySlug(professionSlug);
          const professionSkills = await getProfessionSkillsWithNamesISR(profession.id, 600);
          
          setLoading(false);
          setProfession(profession);
          setProfessionSkills(professionSkills);
      }
      
      getSkillsByProfessionSlug();
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